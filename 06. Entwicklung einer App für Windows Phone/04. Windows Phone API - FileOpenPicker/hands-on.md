Modul 6/04 - Entwicklung einer App für Windows Phone: Windows Phone API - FileOpenPicker
=======================================

##Übersicht
In diesem Hands-On lernen Sie den FileOpenPicker der Windows Phone API kennen und werden ihn dazu verwenden, um ein neues Bild in der App zu posten. Hierzu werden Sie ebenfalls die API-Anbindung zum images-Endpunkt entwickeln.

##Ziele
- Den FileOpenPicker kennenlernen
- Ein Bild vom Mobilgerät auf der API veröffentlichen
- Einen Post auf der API erstellen

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Hinzufügen eines FileOpenPickers</a><br/>
2. <a href="#Exercise2">Post erstellen</a><br/>

<a name="Exercise1"></a>
##Übung 1: Hinzufügen eines FileOpenPickers
In dieser Übung werden Sie der **AddPostPage.xaml** einen FileOpenPicker hinzufügen, um ein Bild auf dem Gerät auswählen zu können.

###Aufgabe 1 - FileOpenPicker aufrufen

1. Öffnen Sie die View **AddPostPage.xaml** im XAML-Designer.
2. Machen Sie sich mit der View vertraut, beachten Sie, dass die **Bild wählen- und Speichern-Schaltflächen** bereits mit Commands im ViewModel verbunden ist.
3. Öffnen Sie das zugehörige ViewModel **AddPostViewModel.cs**.
4. Stellen Sie sicher, dass folgende using-Direktiven im ViewModel enthalten sind:

    ```C#
	using Windows.Storage;
	using Windows.Storage.Pickers;
	using Windows.Storage.Streams;
    ```

5. Navigieren Sie zur Methode, die vom **PickFileCommand** aufgerufen wird.
6. Fügen Sie der Methode **PickImage** den folgenden Code hinzu:

    ```C#
	var openPicker = new FileOpenPicker();
	openPicker.ViewMode = PickerViewMode.Thumbnail;
	openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
	openPicker.FileTypeFilter.Add(".jpg");
	openPicker.FileTypeFilter.Add(".jpeg");
	openPicker.FileTypeFilter.Add(".png");

	// Launch file open picker
	openPicker.PickSingleFileAndContinue();
    ```
	
7. Starten Sie das Debugging und Testen Sie den FileOpenPicker. Sie können, falls Sie den Emulator verwenden, auch die virtuelle Kamera verwenden, um ein Foto zu machen.

Sie haben der View nun einen FileOpenPicker hinzugefügt. In der nächsten Aufgabe wird die ausgewählte Datei abgefragt und im ViewModel gespeichert.

###Aufgabe 2 - Dateireferenz vom FileOpenPicker abfragen
Der FileOpenPicker kehrt nach Auswahl einer Datei auf die Ursprungsseite der App zurück. Die Dateireferenz wird der App im **OnActivated-Ereignis** übergeben.

1. Machen Sie einen Rechtsklick auf die Datei **App.xaml** im **Projektmappen-Explorer** und wählen **Code anzeigen**.
2. Fügen Sie der Klasse **App** den folgenden Code hinzu (überschreiben Sie das **OnActivated** Event falls es bereits vorhanden ist):

	```C#
	protected override void OnActivated(IActivatedEventArgs e)
	{
		// Handle file open picker result
		if (e is FileOpenPickerContinuationEventArgs)
		{
			var args = e as FileOpenPickerContinuationEventArgs;
			if (args.Files.Count > 0)
			{
				// Get picked file
				var pickedFile = args.Files[0];
				
				// Get current page
				var addPostPage = ((Frame)Window.Current.Content).Content as AddPostPage;
				if (addPostPage != null)
				{
					// Pass file to AddPostPage
					addPostPage.ContinueFileOpenPicker(pickedFile);
				}
			}
		}
	}
    ```
	
3. Machen Sie sich mit dem obigen Codeblock und damit der Funktionsweise der FileOpenPicker-API und den FileOpenPickerContinuationEventArgs vertraut.
4. Machen Sie einen Rechtsklick auf die Datei **AddPostPage.xaml** im **Projektmappen-Explorer** und wählen **Code anzeigen**.
5. Fügen Sie der Klasse **AddPostPage** den folgenden Code hinzu:
   
	```C#
	public void ContinueFileOpenPicker(Windows.Storage.StorageFile file)
	{
		this.viewModel.Image = file;
	}
    ```
	
6. Setzen Sie den Cursor auf die Zeile **this.viewModel.Image = file;** und wählen im Menü **Debuggen/Haltepunkt umschalten** oder drücken F9.
7. Starten Sie das Debugging, wählen Sie eine Datei mit dem FileOpenPicker aus und warten, bis der Haltepunkt im obigen Code erreicht wird. Inspizieren Sie das dort zurückgegebene Objekt vom Typ **StorageFile**.<br/><br/>
   ![](images/debugging-storagefile.png?raw=true "Abbildung 1")
8. Entfernen Sie den Haltepunkt wieder, indem Sie F9 in der markierten Zeile drücken.

Es sind nun alle Informationen im ViewModel vorhanden, um einen neuen Post auf der API zu erstellen.

<a name="Exercise2"></a>
##Übung 2: Post erstellen
In dieser Übung werden Sie den images-Endpunkt verwenden, um das ausgewählte Bild auf der API hinzuzufügen. Daraufhin werden Sie auf dem post-Endpunkt einen neuen Post hinzufügen.

###Aufgabe 1 - Bild hinzufügen
Der Prozess sieht vor, dass im ersten Schritt der API ein Bild über den images-Endpunkt hinzugefügt wird, welcher daraufhin die Id des Bildes zurückliefert. Diese kann dann verwendet werden, um einen neuen Post zu erstellen.

1. Öffnen Sie die Datei **AddPostViewModel.cs**.
2. Ersetzen Sie die Methode **AddImageAndPost** durch den folgenden Code:
   
	```C#
	/// <summary>
	/// Adds an image and then the post to the api.
	/// </summary>
	/// <param name="obj"></param>
	private async void AddImageAndPost(object obj)
	{
		var imageId = await this.AddImageAsync();
	}
    ```
	
3. Fügen Sie der Datei die folgenden using-Direktiven hinzu:

    ```C#
	using ImageApp.DataModel;
	using RestSharp.Portable;
    ```
4. Ersetzen Sie die Methode **AddImageAsync** durch den folgenden Code und ersetzen Sie den Platzhalter für die API-Adresse:

	```C#
	/// <summary>
	/// Posts an image to the api.
	/// </summary>
	/// <returns></returns>
	private async Task<int?> AddImageAsync()
	{
		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("images", HttpMethod.Post);
			var data = await ReadFileAsync(this.image);
			request.AddFile(FileParameter.Create("file", data, this.image.Name));

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
	
5. Machen Sie sich mit dem Code zum hinzufügen eines neues Bildes zur API vertraut. Beachten Sie insbesondere den Aufruf der Methode **ReadFileAsync**, die das geladene Bild vom FileOpenPicker in den Speicher lädt.
6. Setzen Sie einen Haltepunkt in die zuvor eingefügte Methode **AddImageAndPost** in die Zeile unterhalb von **var imageId = await this.AddImageAsync();**.
7. Starten Sie das Debugging, navigieren Sie auf die **Post hinzufügen-Seite**, tragen Sie einen Titel ein, wählen ein Bild aus und wählen anschließend **Speichern**. Inspizieren Sie den Wert von **imageId**, der nun von der API zurückgeliefert werden sollte.<br/><br/>
   ![](images/debugging-imageid.png?raw=true "Abbildung 2")

Mit der imageId kann nun ein Post auf der API erstellt werden.

###Aufgabe 2 - Post hinzufügen
In dieser Aufgabe wird nun ein POST auf dem post-Endpunkt durchgeführt, um ein neues Post zu erstellen.

1. Öffnen Sie die Datei **AddPostViewModel.cs**.
2. Erweitern Sie die Methode **AddImageAndPost** durch den folgenden Code:
   
	```C#
	/// <summary>
	/// Adds an image and then the post to the api.
	/// </summary>
	/// <param name="obj"></param>
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

3. Ersetzen Sie die Methode **AddPostAsync** durch den folgenden Code und ersetzen Sie den Platzhalter für die API-Adresse:

	```C#
	/// <summary>
	/// Adds a post to the api.
	/// </summary>
	/// <param name="imageId"></param>
	/// <returns></returns>
	private async Task<bool> AddPostAsync(int imageId)
	{
		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("posts", HttpMethod.Post);

			var addPost = new AddPostRequest { ImageId = imageId, Title = this.title, UserIdentifier = SessionService.DeviceId };
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

4. Machen Sie sich mit den eben eingefügten Methoden vertraut und betrachten Sie den Datenfluss von **AddImageAndPost** bis hin zu **AddPostAsync**.
5. Starten Sie das Debugging und fügen ein neues Bild mit einen beliebigen Titel hinzu.
6. Sie werden automatisch auf die Hauptseite zurückgeleitet. Betrachten Sie dort ihr neu hinzugefügtes Bild.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Die FileOpenPicker-API von Windows Phone zu verwenden
- Dateioperationen auf dem Windows Phone durchzuführen