Modul 6/04 - Entwicklung einer App f�r Windows Phone: Windows Phone API - FileOpenPicker
=======================================

##�bersicht
In diesem Hands-On lernen Sie den FileOpenPicker der Windows Phone API kennen und werden ihn dazu verwenden, um ein neues Bild in der App zu posten. Hierzu werden Sie ebenfalls die API-Anbindung zum images-Endpunkt entwickeln.

##Ziele
- Den FileOpenPicker kennenlernen
- Ein Bild vom Mobilger�t auf der API ver�ffentlichen
- Einen Post auf der API erstellen

##�bungen
Dieses Hands-On besteht aus den folgenden �bungen:<br/>
1. <a href="#Exercise1">Hinzuf�gen eines FileOpenPickers</a><br/>
2. <a href="#Exercise2">Post erstellen</a><br/>

<a name="Exercise1"></a>
##�bung 1: Hinzuf�gen eines FileOpenPickers
In dieser �bung werden Sie der **AddPostPage.xaml** einen FileOpenPicker hinzuf�gen, um ein Bild auf dem Ger�t ausw�hlen zu k�nnen.

###Aufgabe 1 - FileOpenPicker aufrufen

1. �ffnen Sie die View **AddPostPage.xaml** im XAML-Designer.
2. Machen Sie sich mit der View vertraut, beachten Sie, dass die **Bild w�hlen- und Speichern-Schaltfl�chen** bereits mit Commands im ViewModel verbunden ist.
3. �ffnen Sie das zugeh�rige ViewModel **AddPostViewModel.cs**.
4. Stellen Sie sicher, dass folgende using-Direktiven im ViewModel enthalten sind:

    ```C#
	using Windows.Storage;
	using Windows.Storage.Pickers;
	using Windows.Storage.Streams;
    ```

5. Navigieren Sie zur Methode, die vom **PickFileCommand** aufgerufen wird.
6. F�gen Sie der Methode **PickImage** den folgenden Code hinzu:

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
	
7. Starten Sie das Debugging und Testen Sie den FileOpenPicker. Sie k�nnen, falls Sie den Emulator verwenden, auch die virtuelle Kamera verwenden, um ein Foto zu machen.

Sie haben der View nun einen FileOpenPicker hinzugef�gt. In der n�chsten Aufgabe wird die ausgew�hlte Datei abgefragt und im ViewModel gespeichert.

###Aufgabe 2 - Dateireferenz vom FileOpenPicker abfragen
Der FileOpenPicker kehrt nach Auswahl einer Datei auf die Ursprungsseite der App zur�ck. Die Dateireferenz wird der App im **OnActivated-Ereignis** �bergeben.

1. Machen Sie einen Rechtsklick auf die Datei **App.xaml** im **Projektmappen-Explorer** und w�hlen **Code anzeigen**.
2. F�gen Sie der Klasse **App** den folgenden Code hinzu (�berschreiben Sie das **OnActivated** Event falls es bereits vorhanden ist):

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
4. Machen Sie einen Rechtsklick auf die Datei **AddPostPage.xaml** im **Projektmappen-Explorer** und w�hlen **Code anzeigen**.
5. F�gen Sie der Klasse **AddPostPage** den folgenden Code hinzu:
   
	```C#
	public void ContinueFileOpenPicker(Windows.Storage.StorageFile file)
	{
		this.viewModel.Image = file;
	}
    ```
	
6. Setzen Sie den Cursor auf die Zeile **this.viewModel.Image = file;** und w�hlen im Men� **Debuggen/Haltepunkt umschalten** oder dr�cken F9.
7. Starten Sie das Debugging, w�hlen Sie eine Datei mit dem FileOpenPicker aus und warten, bis der Haltepunkt im obigen Code erreicht wird. Inspizieren Sie das dort zur�ckgegebene Objekt vom Typ **StorageFile**.<br/><br/>
   ![](images/debugging-storagefile.png?raw=true "Abbildung 1")
8. Entfernen Sie den Haltepunkt wieder, indem Sie F9 in der markierten Zeile dr�cken.

Es sind nun alle Informationen im ViewModel vorhanden, um einen neuen Post auf der API zu erstellen.

<a name="Exercise2"></a>
##�bung 2: Post erstellen
In dieser �bung werden Sie den images-Endpunkt verwenden, um das ausgew�hlte Bild auf der API hinzuzuf�gen. Daraufhin werden Sie auf dem post-Endpunkt einen neuen Post hinzuf�gen.

###Aufgabe 1 - Bild hinzuf�gen
Der Prozess sieht vor, dass im ersten Schritt der API ein Bild �ber den images-Endpunkt hinzugef�gt wird, welcher daraufhin die Id des Bildes zur�ckliefert. Diese kann dann verwendet werden, um einen neuen Post zu erstellen.

1. �ffnen Sie die Datei **AddPostViewModel.cs**.
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
	
3. F�gen Sie der Datei die folgenden using-Direktiven hinzu:

    ```C#
	using ImageApp.DataModel;
	using RestSharp.Portable;
    ```
4. Ersetzen Sie die Methode **AddImageAsync** durch den folgenden Code und ersetzen Sie den Platzhalter f�r die API-Adresse:

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
	
5. Machen Sie sich mit dem Code zum hinzuf�gen eines neues Bildes zur API vertraut. Beachten Sie insbesondere den Aufruf der Methode **ReadFileAsync**, die das geladene Bild vom FileOpenPicker in den Speicher l�dt.
6. Setzen Sie einen Haltepunkt in die zuvor eingef�gte Methode **AddImageAndPost** in die Zeile unterhalb von **var imageId = await this.AddImageAsync();**.
7. Starten Sie das Debugging, navigieren Sie auf die **Post hinzuf�gen-Seite**, tragen Sie einen Titel ein, w�hlen ein Bild aus und w�hlen anschlie�end **Speichern**. Inspizieren Sie den Wert von **imageId**, der nun von der API zur�ckgeliefert werden sollte.<br/><br/>
   ![](images/debugging-imageid.png?raw=true "Abbildung 2")

Mit der imageId kann nun ein Post auf der API erstellt werden.

###Aufgabe 2 - Post hinzuf�gen
In dieser Aufgabe wird nun ein POST auf dem post-Endpunkt durchgef�hrt, um ein neues Post zu erstellen.

1. �ffnen Sie die Datei **AddPostViewModel.cs**.
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

3. Ersetzen Sie die Methode **AddPostAsync** durch den folgenden Code und ersetzen Sie den Platzhalter f�r die API-Adresse:

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

4. Machen Sie sich mit den eben eingef�gten Methoden vertraut und betrachten Sie den Datenfluss von **AddImageAndPost** bis hin zu **AddPostAsync**.
5. Starten Sie das Debugging und f�gen ein neues Bild mit einen beliebigen Titel hinzu.
6. Sie werden automatisch auf die Hauptseite zur�ckgeleitet. Betrachten Sie dort ihr neu hinzugef�gtes Bild.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Die FileOpenPicker-API von Windows Phone zu verwenden
- Dateioperationen auf dem Windows Phone durchzuf�hren