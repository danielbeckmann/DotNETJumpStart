Modul 6/03 - Entwicklung einer App für Windows Phone: Anbindung der API über Services
=======================================

##Übersicht
In diesem Hands-On werden Sie auf die API mit der App zugreifen und die Testausgaben durch Live-Daten ersetzen. Dazu wird der Abruf der Posts mit den verschiedenen Sortierungen angebunden.

##Ziele
- RestSharp kennenlernen
- Die API anbinden um Posts abzurufen
- Registrierung und Login der API verwenden
- Die Like-Funktion verwenden

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">RestSharp installieren und kennenlernen</a><br/>
2. <a href="#Exercise2">RestSharp verwenden um Posts abzurufen</a><br/>
3. <a href="#Exercise3">Die Registrierung, Login und Like-Funktion anbinden</a><br/>

<a name="Exercise1"></a>
##Übung 1: RestSharp installieren und kennenlernen
In dieser Übung werden Sie RestSharp, eine Bibliothek für REST APIs der App hinzufügen und die Funktionsweise kennenlernen.

###Aufgabe 1 - RestSharp über NuGet installieren
RestSharp kann am einfachsten über NuGet installiert werden.

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **ImageApp** und wählen **NuGet-Pakete verwalten...**".<br/><br/>
   ![](images/manage-nuget-packages.png?raw=true "Abbildung 1")
2. Im Dialogfeld wählen Sie links **Online** aus und geben im Suchfeld **RestSharp.Portable** ein.<br/><br/>
   ![](images/restsharp-nuget.png?raw=true "Abbildung 2")
3. Installieren Sie das Paket **RestSharp.Portable**. Stimmen Sie den Nutzungsbedingungen zu.

Sie können nun RestSharp verwenden, um auf REST APIs zuzugreifen.

###Aufgabe 2 - RestSharp Funktionsweise kennenlernen
Machen Sie sich mit den Funktionen von RestSharp vertraut.

1. Hierzu navigieren Sie in einem Webbrowser auf **http://restsharp.org/**.
2. Sehen Sie sich die Beispiele an und machen sich mit der Funktionsweise von RestSharp vertraut.
3. Fügen Sie der Hauptseite der App testweise einen Button mit einem OnClick-Handler hinzu. Versuchen Sie mittels RestSharp auf den Post-Endpunkt der API zuzugreifen, um Daten abzurufen.

<a name="Exercise2"></a>
##Übung 2: RestSharp verwenden um Posts abzurufen
In dieser Übung werden Sie RestSharp verwenden, um die Posts von der API abzurufen.

###Aufgabe 1 - Laden der Daten vom Posts-Endpunkt
Mit RestSharp können die Posts relativ einfach über einen GET-Aufruf abgerufen werden.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Fügen Sie der Datei die folgenden using-Direktiven hinzu: 

    ```C#
	using RestSharp.Portable;
	using System.Net.Http;
    ```

3. Fügen Sie dem MainViewModel ebenfalls den folgenden Codeblock hinzu und ersetzen Sie den Platzhalter für die API-Adresse:

    ```C#
	/// <summary>
	/// Gets the posts from the api.
	/// </summary>
	public async Task GetPostsAsync()
	{
		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("posts", HttpMethod.Get);
			var result = await client.Execute<List<Post>>(request);
			
			this.Posts = result.Data;
		}
	}
    ```
	
Das MainViewModel verfügt nun über eine Methode, um die Posts von der API abzurufen und in seiner Auflistung zu speichern.

###Aufgabe 2 - GetPostsAsync aufrufen
Die Methode **GetPostsAsync** muss im nächsten Schritt noch aufgerufen werden. Hierzu binden wir den Methodenaufruf in die Hauptseite ein.

1. Machen Sie einen Rechtsklick auf die Datei **MainPage.xaml** im **Projektmappen-Explorer** und wählen **Code anzeigen**.
2. Binden Sie die folgende using-Direktive ein:

    ```C#
	using ImageApp.ViewModels;
	```

3. Überschreiben Sie den Konstruktor der Klasse und die ggf. bereits bestehende Methode **OnNavigatedTo** mit dem folgenden Code:

    ```C#
	private MainViewModel viewModel;

	public MainPage()
	{
		this.InitializeComponent();
		this.NavigationCacheMode = NavigationCacheMode.Required;
		this.viewModel = this.DataContext as MainViewModel;
	}

	protected override async void OnNavigatedTo(NavigationEventArgs e)
	{
		await this.viewModel.GetPostsAsync();
	}
	```

4. Starten Sie das Debugging. Die Live-Daten sollten beim Öffnen der Hauptseite angezeigt werden.

Sie haben damit den Post-Endpunkt in der App angebunden.

###Aufgabe 3 - Sortierung verwenden
Um die Sortierungsfunktion für die Posts zu verwenden, muss ein Parameter an die URL angehängt werden. Auch das kann mit RestSharp elegant gelöst werden.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Fügen Sie der Klasse eine Enumeration hinzu, welche die Sortierung angibt. Fügen Sie ebenfalls eine Variable für die aktuelle Sortierung hinzu:

    ```C#
	public enum Sorting { Latest, Popular };
	private Sorting currentSorting = Sorting.Latest;
    ```
	
3. Ersetzen Sie die Methode **GetPostsAsync** durch den folgenden Codeblock und ersetzen Sie den Platzhalter für die API-Adresse:

    ```C#
	/// <summary>
	/// Gets the posts from the api.
	/// </summary>
	/// <param name="sorting">Current sorting</param>
	public async Task GetPostsAsync(Sorting sorting = Sorting.Latest)
	{
		this.currentSorting = sorting;

		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("posts/{sorting}", HttpMethod.Get);
			request.AddUrlSegment("sorting", sorting.ToString());
			var result = await client.Execute<List<Post>>(request);

			this.Posts = result.Data;
		}
	}
    ```

4. Wenden Sie nun Ihr gewonnenes Wissen an, um die Methoden **SortByRating** und **SortByDate** korrekt zu implementieren.
5. Starten Sie das Debugging und Testen Sie die Sortierung.

Mit Abschluss dieser Übung haben Sie die Sortierungsfunktion für die Posts implementiert.
	
<a name="Exercise3"></a>
##Übung 3: Die Registrierung, Login und Like-Funktion anbinden
In dieser Übung werden Sie die Registrierung und den Login-Vorgang aktivieren, um darauffolgend die Like-Funktion verwenden zu können, welche einen angemeldeten Nutzer voraussetzt.

###Aufgabe 1 - Eindeutige Device-Id setzen
Zur Identifizierung des Benutzers wird von der API die eindeutige Device-Id des Geräts verwendet. Wenn Sie auf dem Emulator, statt auf einem echten Gerät testen, könnte Ihre Device-Id mit anderen Teilnehmern des Workshops identisch sein. Um das zu verhindern, werden Sie die Generierung der Device-Id etwas verändern.  

Falls Sie ein echtes Device zum Testen verwenden, können Sie diese Aufgabe überspringen.

1. Öffnen Sie die Datei **Services/SessionService.cs**.
2. Suchen Sie die Zeile, wo die Device-Id als Zeichenfolge zurückgegeben wird (return deviceId;).
3. Fügen Sie der Rückgabe eine zufällige Zeichenfolge an, um die Device-Id für Ihren Emulator eindeutig zu machen. Beispielsweise: **return deviceId + "Sb1sgyxS!";**.

###Aufgabe 2 - Loginfunktion aktivieren
Der Loginvorgang findet beim Start der App auf dem SplashScreen statt. Dafür muss die bereits bestehende **SplashPage** als Startseite für die App gesetzt werden.

1. Machen Sie einen Rechtsklick auf die Datei **App.xaml** im **Projektmappen-Explorer** und wählen **Code anzeigen**.
2. Rufen Sie den Suchdialog mit **Strg+F** auf und suchen Sie nach "MainPage".
3. Ersetzen Sie MainPage durch "SplashPage".
4. Öffnen Sie den Code der Datei **SplashPage.xaml** und machen Sie sich mit dem Code vertraut. 
5. Öffnen Sie nun die Datei **LoginViewModel.cs** und inspizieren Sie die Login-Funktion, welche auf den **SessionService** zugreift.
6. Öffnen Sie die Datei **SessionService.cs** und ersetzen die Methode **LoginAsync** durch den folgenden Codeblock (denken Sie daran, den Platzhalter für die API-Adresse anzupassen):

    ```C#
	/// <summary>
	/// Performs a login on the api.
	/// </summary>
	/// <returns>True on success</returns>
	public async Task<bool> LoginAsync()
	{
		var deviceId = SessionService.DeviceId;
		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("users/{id}", HttpMethod.Get);
			request.AddUrlSegment("id", deviceId);

			try
			{
				var result = await client.Execute<User>(request);
				SessionService.UserName = result.Data.Name;

				return true;
			}
			catch
			{
				return false;
			}
		}
	}
    ```
	
7. Inspizieren Sie den Code für das Login. 

Um den Login zu ermöglichen, muss ebenfalls eine Registierung bei der API möglich sein.
	
###Aufgabe 2 - Registrierung aktivieren
Die App navigiert automatisch zur View **RegisterPage.xaml**, falls der Login auf dem SplashScreen fehlschlägt. Die dafür benötigte Logik wird in dieser Aufgabe eingefügt.

1. Öffnen Sie die Datei **RegisterPage.xaml**, sowohl im XAML-Designer, als auch in der Code-Ansicht und machen Sie sich mit dem Code vertraut.
2. Öffnen Sie die Datei **SessionService.cs** und ersetzen die Methode **RegisterAsync** durch den folgenden Codeblock (denken Sie daran, den Platzhalter für die API-Adresse anzupassen):

    ```C#
	/// <summary>
	/// Registers the user on the api.
	/// </summary>
	/// <returns>True on success</returns>
	public async Task<bool> RegisterAsync(string userName)
	{
		var user = new User { Identifier = SessionService.DeviceId, Name = userName };
		using (var client = new RestClient("http://localhost:50983/api/"))
		{
			var request = new RestRequest("users", HttpMethod.Post);
			request.AddBody(user);

			try
			{
				var result = await client.Execute(request);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
    ```
	
3. Starten Sie das Debugging und Testen die Registrierung.
4. Beenden Sie das Debugging und Starten es erneut. Sie sollten nun über den SplashScreen automatisch angemeldet werden und auf die Hauptseite der App weitergeleitet werden.

Mit dieser Aufgabe haben Sie den Login und die Registrierung der App aktiviert. Sie können nun auf Funktionen der API zugreifen, die einen registrierten Benutzer erfordern.

###Aufgabe 3 - Den Like-Endpunkt aufrufen und die Anzahl Likes auf der Oberfläche anzeigen
In dieser Aufgabe werden Sie den Like-Endpunkt der API anbinden und die Anzeige der Posts auf der Hauptseite dahingehend erweitern, so dass die Anzahl der Likes angezeigt werden kann.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Fügen Sie der Klasse ein neues Attribut zu, welches den aktuell angezeigten Post speichert:

    ```C#
	private Post currentPost;

	/// <summary>
	/// Gets or sets the current post.
	/// </summary>
	public Post CurrentPost
	{
		get { return currentPost; }
		set { this.SetProperty(ref this.currentPost, value); }
	}
    ```
	
3. Ersetzen Sie den Code der Methode **LikePostAsync** durch den folgenden und ersetzen Sie den Platzhalter für die API-Adresse:

    ```C#
	/// <summary>
	/// Toggles the like of the current post on the api.
	/// </summary>
	public async Task LikePostAsync()
	{
		var like = new Like 
		{
			PostId = this.CurrentPost.Id,
			UserIdentifier = SessionService.DeviceId
		};

		using (var client = new RestClient("http://{API-Adresse}/api/"))
		{
			var request = new RestRequest("likes", HttpMethod.Post);
			request.AddBody(like);
			var result = await client.Execute(request);
			
			if (result.StatusCode == System.Net.HttpStatusCode.Created)
			{
				this.CurrentPost.Likes++;
			}
			else
			{
				this.CurrentPost.Likes--;
			}
		}
	}
    ```

4. Machen Sie sich mit dem Code zum Liken eines Posts vertraut.
5. Öffnen Sie die **MainPage.xaml** im XAML-Designer.
6. Ersetzen Sie den Code des Pivots, in dem die Posts angezeigt werden durch den Folgenden:
	
    ```XML  
	<Pivot x:Name="PostPivot" Grid.Row="1" Margin="0,27,0,0" ItemsSource="{Binding Posts}" SelectedItem="{Binding CurrentPost, Mode=TwoWay}">
            <Pivot.HeaderTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding Title}" FontSize="42" />
                </DataTemplate>
            </Pivot.HeaderTemplate>
            <Pivot.ItemTemplate>
                <DataTemplate>
                    <Grid>
                        <Grid.RowDefinitions>
                            <RowDefinition Height="32" />
                            <RowDefinition Height="*" />
                        </Grid.RowDefinitions>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="60" />
                            <ColumnDefinition Width="*" />
                        </Grid.ColumnDefinitions>

                        <TextBlock Style="{StaticResource BaseTextBlockStyle}"
                                   Text="Likes: "/>
                        <TextBlock Grid.Column="1" Style="{StaticResource BaseTextBlockStyle}"
                                   Text="{Binding Likes}" />
                        <Image Grid.Row="1" Grid.ColumnSpan="2"
                               Source="{Binding ImageUri}"/>
                    </Grid>
                </DataTemplate>
            </Pivot.ItemTemplate>
        </Pivot>
    ```

7. Inspizieren Sie den neuen XAML-Code und machen Sie sich mit den Änderungen vertraut.
8. Starten Sie das Debugging und Testen Sie die Like-Funktion
	
##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Die RestSharp-Bibliothek zu verwenden  
- Auf eine API zuzugreifen