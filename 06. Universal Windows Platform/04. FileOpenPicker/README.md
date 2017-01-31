# Modul 6/04 - Entwicklung einer App f�r die universelle Windows Plattform: Windows APIs - Der FileOpenPicker

## �bersicht

In diesem Hands-On lernen Sie den FileOpenPicker der Windows API kennen und werden ihn dazu verwenden,
 ein neues Bild �ber die App hochzuladen. Hierzu werden Sie ebenfalls die API-Anbindung zum images-Endpunkt umsetzen.

## Ziele

- Den FileOpenPicker kennenlernen
- Ein Bild �ber die App hochzuladen
- Einen Post �ber die App zu erstellen

---

## �bungen

Dieses Hands-On besteht aus den folgenden �bungen:<br/>
1. <a href="#Exercise1">Hinzuf�gen des FileOpenPickers</a><br/>
2. <a href="#Exercise2">Post erstellen</a><br/>

<a name="Exercise1"></a>
### �bung 1: Hinzuf�gen eines FileOpenPickers
In dieser �bung werden Sie der **AddPostPage.xaml** einen FileOpenPicker hinzuf�gen, um ein Bild auf dem Ger�t ausw�hlen zu k�nnen.

#### Aufgabe 1 - FileOpenPicker aufrufen

1. �ffnen Sie die Datei **AddPostPage.xaml** im XAML-Designer.
2. Machen Sie sich mit der View vertraut. Beachten Sie, dass die **Bild w�hlen- und Speichern-Schaltfl�chen** bereits mit Commands in einem ViewModel verbunden ist.
3. �ffnen Sie das zugeh�rige ViewModel **AddPostViewModel.cs** und machen sich mit den Commands vertraut.
4. Stellen Sie sicher, dass folgende using-Direktiven im ViewModel enthalten sind:

    ```C#
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    ```

5. F�gen Sie der Methode **PickImage** den folgenden Code hinzu:

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
### �bung 2: Post erstellen
In dieser �bung werden Sie den images-Endpunkt verwenden, um das ausgew�hlte Bild auf die API hochzuladen. Daraufhin werden Sie auf dem post-Endpunkt einen neuen Post hinzuf�gen.

#### Aufgabe 1 - Bild hochladen
Der Prozess sieht vor, dass im ersten Schritt der API ein Bild �ber den images-Endpunkt hochgeladen wird, welcher daraufhin die Id des Bildes zur�ckliefert. Diese kann dann verwendet werden, um einen neuen Post zu erstellen.

1. �ffnen Sie die Datei **AddPostViewModel.cs**.
2. Machen Sie sich mit der Methode **AddImageAndPost** vertraut. Diese ruft im ersten Schritt die Methode **AddImageAsync** auf. Diese soll zum Hochladen des Bildes verwendet werden und die Id des Bildes zur�ckliefern.
   
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
	
3. F�gen Sie der Datei die folgenden using-Direktiven hinzu:

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
	
5. Machen Sie sich mit dem Code zum Hochladen eines neues Bildes zur API vertraut. Beachten Sie insbesondere den Aufruf der Methode **ReadFileAsync**, die das geladene Bild vom FileOpenPicker in den Speicher l�dt.
6. Setzen Sie einen Haltepunkt in die Methode **AddImageAndPost** in die Zeile unterhalb von **var imageId = await this.AddImageAsync();** (�ber die Taste **F9**)
7. Starten Sie das Debugging und navigieren Sie auf die **Post hinzuf�gen-Seite**. Tragen Sie einen beliebigen Titel ein, w�hlen ein Bild aus und w�hlen anschlie�end **Speichern**. 
8. Der Debugger sollte automatisch anhalten und Sie k�nnen den Wert von **imageId** auslesen, der nun von der API zur�ckgeliefert werden sollte.<br/><br/>
   ![](_images/debugging-imageid.png?raw=true "Abbildung 1")

Mit der imageId kann nun ein Post-Eintrag auf der API erstellt werden.

#### Aufgabe 2 - Post hinzuf�gen
In dieser Aufgabe wird nun der post-Endpunkt verwendet, um ein neues Post zu erstellen.

1. �ffnen Sie die Datei **AddPostViewModel.cs**.
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
4. Starten Sie das Debugging und f�gen ein neues Bild mit einen beliebigen Titel hinzu.
5. Sie werden nach dem erfolgreichen Speichern automatisch auf die Hauptseite zur�ckgeleitet. Betrachten Sie dort ihr neu hinzugef�gtes Bild.

### Zusammenfassung

Mit Beendung dieser Session haben Sie gelernt:  
- Die FileOpenPicker-API von Windows zu verwenden
- Dateioperationen durchzuf�hren
- Dateien an eine API zu �bermitteln