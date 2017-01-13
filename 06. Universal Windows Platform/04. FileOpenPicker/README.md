# Modul 6/04 - Entwicklung einer App für die universelle Windows Plattform: Windows APIs - Der FileOpenPicker

## Übersicht

In diesem Hands-On lernen Sie den FileOpenPicker der Windows API kennen und werden ihn dazu verwenden,
 ein neues Bild über die App hochzuladen. Hierzu werden Sie ebenfalls die API-Anbindung zum images-Endpunkt umsetzen.

## Ziele

- Den FileOpenPicker kennenlernen
- Ein Bild über die App hochzuladen
- Einen Post über die App zu erstellen

---

## Übungen

Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Hinzufügen des FileOpenPickers</a><br/>
2. <a href="#Exercise2">Post erstellen</a><br/>

<a name="Exercise1"></a>
### Übung 1: Hinzufügen eines FileOpenPickers
In dieser Übung werden Sie der **AddPostPage.xaml** einen FileOpenPicker hinzufügen, um ein Bild auf dem Gerät auswählen zu können.

#### Aufgabe 1 - FileOpenPicker aufrufen

1. Öffnen Sie die Datei **AddPostPage.xaml** im XAML-Designer.
2. Machen Sie sich mit der View vertraut. Beachten Sie, dass die **Bild wählen- und Speichern-Schaltflächen** bereits mit Commands in einem ViewModel verbunden ist.
3. Öffnen Sie das zugehörige ViewModel **AddPostViewModel.cs** und machen sich mit den Commands vertraut.
4. Stellen Sie sicher, dass folgende using-Direktiven im ViewModel enthalten sind:

    ```C#
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    ```

5. Fügen Sie der Methode **PickImage** den folgenden Code hinzu:

    ```C#
    private async void PickImage(object obj)
    {
        var picker = new FileOpenPicker();
        picker.ViewMode = PickerViewMode.Thumbnail;
        picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");

        StorageFile file = await picker.PickSingleFileAsync();
        if (file != null)
        {
            var stream = await file.OpenAsync(FileAccessMode.Read);
            var image = new BitmapImage();
            image.SetSource(stream);

            // Set file reference
            this.imageFile = file;

            // Set preview image
            this.PreviewImage = image;
        }
    }
    ```
	
6. Machen Sie sich mit dem Code vertraut. Die Eigenschaft **PreviewImage** ist bereits an ein **Image-Control** auf der AddPostPage gebunden.
7. Starten Sie das Debugging und Testen Sie den FileOpenPicker und das Vorschaubild.

<a name="Exercise2"></a>
### Übung 2: Post erstellen
In dieser Übung werden Sie den images-Endpunkt verwenden, um das ausgewählte Bild auf die API hochzuladen. Daraufhin werden Sie auf dem post-Endpunkt einen neuen Post hinzufügen.

#### Aufgabe 1 - Bild hochladen
Der Prozess sieht vor, dass im ersten Schritt der API ein Bild über den images-Endpunkt hochgeladen wird, welcher daraufhin die Id des Bildes zurückliefert. Diese kann dann verwendet werden, um einen neuen Post zu erstellen.

1. Öffnen Sie die Datei **AddPostViewModel.cs**.
2. Machen Sie sich mit der Methode **AddImageAndPost** vertraut. Diese ruft im ersten Schritt die Methode **AddImageAsync** auf. Diese soll zum Hochladen des Bildes verwendet werden und die Id des Bildes zurückliefern.
   
	```C#
    private async void AddImageAndPost(object obj)
    {
        var imageId = await this.AddImageAsync();
        if (imageId != null)
        {
            if (await this.AddPostAsync(imageId.Value))
            {
                NavigationService.Current.GoBack();
            }
        }
    }
    ```
	
3. Fügen Sie der Datei die folgenden using-Direktiven hinzu:

    ```C#
    using RestSharp.Portable.HttpClient;
    using RestSharp.Portable;
    using ImageApp.DataModel;
    ```
4. Ersetzen Sie die Methode **AddImageAsync** durch den folgenden Code und ersetzen Sie ggf. die API-Adresse:

	```C#
    /// <summary>
    /// Posts an image to the api.
    /// </summary>
    /// <returns></returns>
    private async Task<int?> AddImageAsync()
    {
        using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
        {
            var request = new RestRequest("images", Method.POST);
            var data = await ReadFileAsync(this.imageFile);
            request.AddFile(FileParameter.Create("file", data, this.imageFile.Name));

            try
            {
                var image = await client.Execute<Image>(request);
                return image.Data.Id;
            }
            catch
            {
                return null;
            }
        }
    }
    ```
	
5. Machen Sie sich mit dem Code zum Hochladen eines neues Bildes zur API vertraut. Beachten Sie insbesondere den Aufruf der Methode **ReadFileAsync**, die das geladene Bild vom FileOpenPicker in den Speicher lädt.
6. Setzen Sie einen Haltepunkt in die Methode **AddImageAndPost** in die Zeile unterhalb von **var imageId = await this.AddImageAsync();** (Über die Taste **F9**)
7. Starten Sie das Debugging und navigieren Sie auf die **Post hinzufügen-Seite**. Tragen Sie einen beliebigen Titel ein, wählen ein Bild aus und wählen anschließend **Speichern**. 
8. Der Debugger sollte automatisch anhalten und Sie können den Wert von **imageId** auslesen, der nun von der API zurückgeliefert werden sollte.<br/><br/>
   ![](_images/debugging-imageid.png?raw=true "Abbildung 1")

Mit der imageId kann nun ein Post-Eintrag auf der API erstellt werden.

#### Aufgabe 2 - Post hinzufügen
In dieser Aufgabe wird nun der post-Endpunkt verwendet, um ein neues Post zu erstellen.

1. Öffnen Sie die Datei **AddPostViewModel.cs**.
2. Ersetzen Sie die Methode **AddPostAsync** durch den folgenden Code und ersetzen Sie ggf. die API-Adresse:

	```C#
    /// <summary>
    /// Adds a post to the api.
    /// </summary>
    /// <param name="imageId"></param>
    /// <returns></returns>
    private async Task<bool> AddPostAsync(int imageId)
    {
        using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
        {
            var request = new RestRequest("posts", Method.POST);

            var addPost = new Post { ImageId = imageId, Title = this.title, UserIdentifier = DeviceUtils.DeviceId };
            request.AddJsonBody(addPost);

            try
            {
                await client.Execute(request);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    ```

3. Machen Sie sich mit dem Code vertraut und betrachten Sie den Datenfluss der imageId von **AddImageAndPost** bis hin zu **AddPostAsync**.
4. Starten Sie das Debugging und fügen ein neues Bild mit einen beliebigen Titel hinzu.
5. Sie werden nach dem erfolgreichen Speichern automatisch auf die Hauptseite zurückgeleitet. Betrachten Sie dort ihr neu hinzugefügtes Bild.

### Zusammenfassung

Mit Beendung dieser Session haben Sie gelernt:  
- Die FileOpenPicker-API von Windows zu verwenden
- Dateioperationen durchzuführen
- Dateien an eine API zu übermitteln