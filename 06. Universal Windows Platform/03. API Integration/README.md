# Modul 6/03 - Entwicklung einer App für die universelle Windows Plattform: Integration von APIs

## Übersicht

In diesem Hands-On werden Sie mit der App auf die API zugreifen und die bisherigen Testdaten durch Livedaten ersetzen. Hierzu wird der Abruf der Posts mit den verschiedenen Sortierungen angebunden.

## Ziele

- RestSharp kennenlernen
- Posts von der API abzurufen
- Die Like-Funktion über die API zu verwenden

---

## Übungen

Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">RestSharp installieren und kennenlernen</a><br/>
2. <a href="#Exercise2">RestSharp verwenden um Posts abzurufen</a><br/>
3. <a href="#Exercise3">Die Registrierung, Login und Like-Funktion anbinden</a><br/>

<a name="Exercise1"></a>
### Übung 1: RestSharp installieren und kennenlernen
In dieser Übung werden Sie RestSharp, eine Bibliothek für REST APIs, zur App hinzufügen und dessen Funktionsweise kennenlernen.

#### Aufgabe 1 - RestSharp über NuGet installieren
RestSharp kann am einfachsten über NuGet installiert werden.

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **ImageApp** und wählen **NuGet-Pakete verwalten...**".<br/><br/>
   ![](_images/manage-nuget-packages.png?raw=true "Abbildung 1")
2. Im Dialogfeld wählen Sie links **Online** aus und geben im Suchfeld **FubarCoder.RestSharp.Portable** ein.<br/><br/>
   ![](_images/restsharp-nuget.png?raw=true "Abbildung 2")
3. Installieren Sie das Paket **FubarCoder.RestSharp.Portable**. Stimmen Sie den Nutzungsbedingungen zu.

Sie können nun RestSharp verwenden, um auf REST APIs zuzugreifen.

#### Aufgabe 2 - RestSharp Funktionsweise kennenlernen
Machen Sie sich mit den Funktionen von RestSharp vertraut.

1. Hierzu navigieren Sie in einem Webbrowser auf **https://github.com/FubarDevelopment/restsharp.portable**.
2. Sehen Sie sich das Beispiel an und machen sich mit der Funktionsweise von RestSharp vertraut.

<a name="Exercise2"></a>
### Übung 2: RestSharp verwenden um Posts abzurufen
In dieser Übung werden Sie RestSharp verwenden, um die Posts von der API abzurufen.

#### Aufgabe 1 - Laden der Daten vom Posts-Endpunkt
Mit RestSharp können die Posts sehr einfach über einen GET-Aufruf abgerufen werden.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Fügen Sie der Datei die folgenden using-Direktiven hinzu: 

    ```C#
    using RestSharp.Portable.HttpClient;
    using RestSharp.Portable;
    ```

3. Fügen Sie dem MainViewModel den folgenden Codeblock hinzu und ersetzen Sie ggf. die API-Adresse:

    ```C#
	/// <summary>
    /// Gets the posts from the api.
    /// </summary>
    public async Task GetPostsAsync()
    {
        using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
        {
            var request = new RestRequest("posts", Method.GET);
            var result = await client.Execute<List<Post>>(request);

            this.Posts = result.Data;
        }
    }
    ```
	
4. Machen Sie sich mit dem eingefügtem Code vertraut und versuchen diesen nachzuvollziehen.

Das MainViewModel verfügt nun über eine Methode, um die Posts von der API abzurufen und in seiner Auflistung zu speichern.

#### Aufgabe 2 - GetPostsAsync aufrufen
Die Methode **GetPostsAsync** muss im nächsten Schritt noch aufgerufen werden. Hierzu binden wir den Methodenaufruf in die Hauptseite ein.

1. Machen Sie einen Rechtsklick auf die Datei **MainPage.xaml** im **Projektmappen-Explorer** und wählen **Code anzeigen**.
2. Binden Sie die folgende using-Direktive ein:

    ```C#
	using ImageApp.ViewModels;
	```

3. Entfernen Sie den bereits bestehenden Konstruktor der Klasse und ersetzen ihn durch folgenden Code:

    ```C#
	private MainViewModel viewModel;

    public MainPage()
    {
        this.InitializeComponent();
        this.viewModel = this.DataContext as MainViewModel;
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        await this.viewModel.GetPostsAsync();
    }
	```

4. Starten Sie das Debugging. Die Livedaten sollten beim Öffnen der Hauptseite geladen und angezeigt werden.

Sie haben damit den Post-Endpunkt in der App angebunden.

#### Aufgabe 3 - Sortierung verwenden
Um die Sortierungsfunktion der API für die Post-Einträge zu verwenden, muss ein Parameter an die URL angehängt werden. Auch das kann mit RestSharp elegant gelöst werden.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Fügen Sie der Klasse eine Enumeration hinzu, welche die Sortierungsarten angibt. Fügen Sie ebenfalls eine Variable für die aktuelle Sortierung hinzu:

    ```C#
	public enum Sorting { Latest, Popular };
	private Sorting currentSorting = Sorting.Latest;
    ```
	
3. Ersetzen Sie die Methode **GetPostsAsync** durch den folgenden Codeblock und ersetzen ggf. den Platzhalter für die API-Adresse. Vergleichen Sie die alte und neue Implementierung der Methode. Die Methode enthält nun einen optionalen Parameter zur Sortierung.

    ```C#
	/// <summary>
	/// Gets the posts from the api.
	/// </summary>
	/// <param name="sorting">Current sorting</param>
	public async Task GetPostsAsync(Sorting sorting = Sorting.Latest)
	{
	    this.currentSorting = sorting;

	    using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
	    {
		    var request = new RestRequest("posts/{sorting}", HttpMethod.Get);
		    request.AddUrlSegment("sorting", sorting.ToString());
		    var result = await client.Execute<List<Post>>(request);

		    this.Posts = result.Data;
	    }
	}
    ```

4. Implementieren Sie den fehlenden Code der beiden Methoden **SortByRating** und **SortByDate** für die beiden Schaltflächen auf der Hauptseite, indem Sie von dort aus die Methode **GetPostsAsync** mit der jeweiligen Sortierung aufrufen.
5. Starten Sie das Debugging und Testen Sie die Sortierung.

Mit Abschluss dieser Übung haben Sie die Sortierungsfunktion für die Posts implementiert.
	
<a name="Exercise3"></a>
### Übung 3: Die Like-Funktion anbinden
In dieser Übung werden Sie die Like-Funktion der API anbinden. Zur Identifikation eines Benutzers wird eine sogenannte Device-Id verwendet.

#### Aufgabe 1 - Die Device-Id kennenlernen
Damit ein Benutzer nicht beliebig viele Likes abgeben kann, muss er eindeutig identifiziert werden. Hierzu werden üblicherweise Registrierung und Login der App-Benutzer vorausgesetzt. Der Einfachheit halber verwenden wir eine **Device-Id**, die ein Gerät eindeutig identifizieren kann.

1. Öffnen Sie die Datei **Utils/DeviceUtils.cs** und verschaffen sich einen Überblick über den Code. Diese Datei haben Sie in einem vorherigen Modul hinzugefügt.

Dieser Code verwendet die im Modul 6/01 hinzugefügten **Windows Mobile Extensions for the UWP**, um eine eindeutige Geräteidentifikation zu erzeugen.

#### Aufgabe 2 - Den Like-Endpunkt aufrufen und die Anzahl Likes auf der Oberfläche anzeigen
In dieser Aufgabe werden Sie den Like-Endpunkt der API anbinden und die Anzeige der Posts auf der Hauptseite erweitern, so dass die Anzahl der Likes angezeigt werden kann.

1. Öffnen Sie die Datei **MainViewModel.cs**.
2. Binden Sie die folgende using-Direktive ein:

    ```C#
	using ImageApp.Utils;
	```
3. Ersetzen Sie den Code der Methode **Like** durch den nachfolgenden Codeblock und ersetzen Sie ggf. die API-Adresse:

    ```C#
    private async void Like(object obj)
    {
        var post = obj as Post;
        if (post != null)
        {
            await this.LikePostAsync(post);
        }
    }

    /// <summary>
    /// Toggles the like of the current post on the api.
    /// </summary>
    public async Task LikePostAsync(Post post)
    {
        var like = new Like
        {
            PostId = post.Id,
            UserIdentifier = DeviceUtils.DeviceId
        };

        using (var client = new RestClient("http://acando-workshop.azurewebsites.net/api/"))
        {
            var request = new RestRequest("likes", Method.POST);
            request.AddBody(like);

            var result = await client.Execute(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                post.Likes++;
            }
            else
            {
                post.Likes--;
            }
        }
    }
    ```

4. Machen Sie sich mit dem Code zum Liken eines Posts vertraut. Mit dem Befehl **DeviceUtils.DeviceId** wird die oben erwähnte Device-Id abgerufen.
5. Öffnen Sie die **MainPage.xaml** im XAML-Designer.
6. Fügen Sie dem **DataTemplate** des **GridView** unterhalb des **Image** den folgenden Codeblock hinzu:
	
    ```XML  
	<StackPanel Grid.Row="2" Margin="0,12,0,0" Orientation="Horizontal">
        <Button Content="&#xE19F;" FontFamily="Segoe UI Symbol" Style="{StaticResource TextBlockButtonStyle}"
                Command="{Binding DataContext.LikeCommand, ElementName=mainPage}" CommandParameter="{Binding}"/>
        <TextBlock FontSize="18" Margin="0,6,0,0">
            <Run Text="(" />
            <Run Text="{Binding Likes}" />
            <Run Text=")" />
        </TextBlock>
    </StackPanel>
    ```

7. Starten Sie das Debugging und Testen Sie die Like-Funktion
	

### Zusammenfassung

Mit Beendung dieser Session haben Sie gelernt:  
- Die RestSharp-Bibliothek zu verwenden  
- Wie man auf eine API zugreift und die Daten in einer App anzeigt