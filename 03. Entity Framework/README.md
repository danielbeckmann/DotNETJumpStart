# Modul 3 - Datenmodellierung und -abfrage mit dem Entity Framework: Erstellen des Datenmodells

## Übersicht 

In diesem Modul werden Sie die Grundlagen zum Entity Framework kennenlernen.

TODO: Ziele dieses Modus
TODO: Ankersprünge zu Übungen

## Präsentation

Sehen Sie sich die zu dem Modul gehörende [Präsentation](03. Datenmodellierung und -abfrage mit dem Entity Framework) an.

## Ziele

- Grundlagen: Entity Framework (Model-First, Database-First, Code-First)
- Grundlagen: Datenzugriffsmethoden
- Grundlagen: Die Razor Syntax und View Rendering

---

## Übungen

1. Erstellen des Datenmodells
2. Datenzugriff
3. Datenanzeige mit der Razor-Syntax

### Übung 1: Erstellen des Datenmodells

In dieser Übung werden wir:
- Das Datenmodell mit Code First erstellen
- Eigenschaften zu Entitäten hinzufügen
- Testdaten erzeugen
- Eine Verbindung zur LocalDB herstellen

![](_images/dbmodel.png?raw=true "Abbildung 1")
Abbildung 1: Das fertige Datenmodell

#### Aufgabe 1 - Hinzufügen der Entitäten

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Models** und wählen **Hinzufügen/Vorhandenes** Element
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Models** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/start.png?raw=true "Abbildung 2")

#### Aufgabe 2 - Hinzufügen weiterer Eigenschaften

1. Öffnen Sie die Aufgabenliste über die Menüleiste **Ansicht/Aufgabenleiste** oder drücken Sie die Tasten **STRG+W, T**
2. Das Aufgabenfenster sollte wie folgt aussehen:

![](_images/todos.png?raw=true "Abbildung 3")

3. Doppelklicken Sie auf den ersten Eintrag oder öffnen Sie die Datei **Models/User.cs**

![](_images/user-entity.png?raw=true "Abbildung 4")

4. Fügen Sie der Klasse User die Eigenschaft **Name** vom Typ **String** mit den Attributen **Required**, **MaxLength = 50** und **Unique** hinzu. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft „Identifier“.

    ```C#
    /// <summary>
    /// Gets or sets the users name
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Index(IsUnique = true)]
    public string Name { get; set; }
    ```
	
5. Fügen Sie der Klasse User die Eigenschaft **Posts** hinzu. Diese Eigenschaft stellt eine Eigenschaft aller vom Benutzer geposteten Einträge dar. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft „Likes“.	

	```C#
    /// <summary>
    /// Gets or sets the users posts.
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; }
	```
	
6. Erstellen Sie die Projektmappe über die Menüleiste **Erstellen/Projektmappe** neu erstellen und stellen Sie sicher, dass keine Fehler auftreten.
	

#### Aufgabe 3 - DbContext bearbeiten 

1. Erzeugen Sie einen neuen Ordner **DataContext** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **DataContext** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/DataContext** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/dbcontext.png?raw=true "Abbildung 5")

5. Öffnen Sie in der Aufgabenliste die erste Aufgabe oder öffnen Sie die Datei **ImageAppDbContext.cs**
6. Fügen Sie der **ImageAppDbContext** Klasse die Set-Eigenschaften für die Entitäten **User**, **Post** und **Like** hinzu. Orientieren Sie sich dabei an der bestehen Eigenschaft zur Entität **_images**

	```C#
    public DbSet<Post> Posts { get; set; }

    public DbSet<Like> Likes { get; set; }

    public DbSet<User> Users { get; set; }
	```
	
7. Öffnen Sie die Datei **ImageAppDbInitializer.cs**
8. Ersetzen Sie den Rumpf der Methode **Seed** mit folgendem Inhalt:

	```C#
    protected override void Seed(ImageAppDbContext context)
    {
        // Create some demo users
        var users = new List<User>
        {
            new User { Identifier = "6fd7d591-0470-41b6-a7a7-0e040bd16638", Name = "Admin" },
            new User { Identifier = "6fd7d591-0470-41b6-a7a7-0e040bd16639", Name = "Dilbert" }
        };

        users.ForEach(s => context.Users.Add(s));
        context.SaveChanges();

        // Create some _images
        var _images = new List<Image>
        {
            new Image { FileName = "business-q-c-1024-768-9.jpg" },
            new Image { FileName = "cats-q-c-1024-768-4.jpg" },
            new Image { FileName = "city-q-c-1024-768-9.jpg" },
            new Image { FileName = "sports-q-c-1024-768-4.jpg" },
            new Image { FileName = "technics-q-c-1024-768-4.jpg" }
        };

        _images.ForEach(s => context._images.Add(s));
        context.SaveChanges();

        // Create some posts
        var posts = new List<Post>
        {
            new Post { Title = "Business stuff", User = users.First(), Image = _images.First() },
            new Post { Title = "My cat", User = users.First(), Image = _images.Skip(1).First() },
            new Post { Title = "Random bridge", User = users.Skip(1).First(), Image = _images.Skip(2).First() },
            new Post { Title = "Surfin' U.S.A.", User = users.Skip(1).First(), Image = _images.Skip(3).First() },
            new Post { Title = "Technics", User = users.First(), Image = _images.Skip(4).First() }
        };

        posts.ForEach(s => context.Posts.Add(s));
        context.SaveChanges();

        // Create some likes
        var likes = new List<Like>
        {
            new Like { Post = posts.Skip(4).First(), User = users.First() },
            new Like { Post = posts.Skip(4).First(), User = users.Skip(1).First() },
            new Like { Post = posts.Skip(2).First(), User = users.First() },
        };

        likes.ForEach(s => context.Likes.Add(s));
        context.SaveChanges();
    }
	```
	
9. Erstellen Sie die Projektmappe über die Menüleiste **Erstellen/Projektmappe** neu erstellen und stellen Sie sicher, dass keine Fehler auftreten.

#### Aufgabe 4 - Testbilder bereitstellen
1. Erzeugen Sie einen neuen Ordner **Uploads** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Uploads** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Uploads** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/uploads.png?raw=true "Abbildung 6")

#### Aufgabe 5 - DbContext und DbInitializer bekannt machen 
1. Öffnen Sie die Datei **Web.config** im **Stammverzeichnis** Ihrer Projektmappe
2. Ersetzen Sie die Konfigurationssektion **entityFramework** mit folgendem Inhalt

	```XML
    <entityFramework>
    <contexts>
      <context type="WebAdminAndApi.Models.ImageAppDbContext, DotNETJumpStart">
        <databaseInitializer type="WebAdminAndApi.Models.ImageAppDbInitializer, DotNETJumpStart" />
      </context>
    </contexts>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
    </entityFramework>
	```
	
3. Speichern Sie Ihre Änderungen und erstellen die Projektmappe neu.

### Übung 2: Datenzugriff

In dieser Übung werden wir:
- Ein Bild aus einem HttpRequest speichern
- Einem Bild ein Wasserzeichen geben
- Daten festlegen, die ein View anzeigen sollte

#### Aufgabe 1 - Hinzufügen der ImageUtils und Bearbeiten der Controller

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Erzeugen Sie einen neuen Ordner **Utils** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
3. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Utils** und wählen **Hinzufügen/Vorhandenes Element**.
4. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Utils** aus dem aktuellen Hands-On und wählen alle Dateien aus.
5. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer.png?raw=true "Abbildung 1")

6. Öffnen Sie die Datei **Utils/ImageUtility.cs** oder öffnen Sie in der Aufgabenliste die erste Aufgabe 
7. Navigieren Sie zu der Funktion **ResizeImageAndSaveToDisk**
8. Verpassen Sie den hochgeladenen Bildern ein Wasserzeichen mit **Ihrem Namen**

    ```C#
	webImage = webImage.AddTextWatermark(".NET Jumpstart");
    ```
	
9. Speichern und schließen Sie die Datei

#### Aufgabe 2 - HomeController bearbeiten
1. Öffnen Sie die Datei **Controllers/HomeController**
2. Stellen Sie sicher, dass folgende using-Direktiven im HomeController enthalten sind:

    ```C#
	using WebAdminAndApi.Models;
    ```
	
3. Ändern Sie die Methode **Index()** so ab, dass Sie nur die 10 neuesten Posts anzeigt

    ```C#
	private ImageAppDbContext db = new ImageAppDbContext();

	public ActionResult Index()
	{
		var posts = db.Posts.OrderByDescending(p => p.Created).Take(10);
		return View(posts.ToList());
	}
	```
	
4. Speichern und schließen Sie die Datei

#### Aufgabe 3 - PostsController hinzufügen
1. Machen Sie einen Rechtsklick auf den neu Ordner **Controllers** und wählen **Hinzufügen/Vorhandenes Element**.
2. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Controllers** aus dem aktuellen Hands-On und wählen alle Dateien aus.
3. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer-2.png?raw=true "Abbildung 2")

4. Starten Sie Ihre Anwendung über die Menüleiste **Debuggen/Debugging starten** oder das Tastenkürzel **F5**

![](_images/start-asp-net.png?raw=true "Abbildung 3")

5. Beenden Sie das Debugging durch das Schließen des Browsers oder über die Menüleiste **Debugging/Debugging beenden** innerhalb von Visual Studio

#### Aufgabe 4 - Tabelldaten anzeigen
1. Öffnen Sie den **Server-Explorer**
2. Suchen Sie die zuvor hinzugefügte Verbindung zur lokalen Datenbank und klappen Sie sie auf
3. Klappen Sie die 
4. Ihr Server-Explorer sollte nun wie folgt aussehen:

![](_images/table-view.png?raw=true "Abbildung 9")

5. Machen Sie einen Rechtsklick auf die Tabelle **Post** und wählen den Menüeintrag **Tabellendaten anzeigen**
6. Ihre Projektmappe sollte nun wie folgt aussehen

![](_images/data-view.png?raw=true "Abbildung 10")

### Übung 3: Datenanzeige mit der Razor-Syntax

In dieser Übung werden wir:
- Die Razor-Syntax verwenden, um auf Eigenschaften eines ViewModels zuzugreifen
- HTML verwenden, um Daten aus einem ViewModel anzuzeigen

#### Aufgabe 1 - Posts auf der Startseite anzeigen
1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Öffnen Sie die Datei **Views/Home/Index.cshtml**
3. Ersetzen Sie den Inhalt der Datei mit folgendem, um die Daten aus der zuvor angepassten **Index**-Methode des **HomeController**s anzuzeigen:

	```XML
	@model IEnumerable<WebAdminAndApi.Models.Post>
	@{
		ViewBag.Title = "Übersicht";
	}
	<h3>Die letzten Postings:</h3>
	<ul>
		@foreach (var item in Model)
		{
			<li>
				<h5>@Html.DisplayFor(modelItem => item.Title) <small>von @Html.DisplayFor(m => item.User.Name)</small></h5> <br />
				<img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" style="vertical-align:middle" />
			</li>
		}
	</ul>
	```
	
4. Speichern Sie die Datei.
5. Starten Sie Ihre Anwendung über das Tastenkürzel **F5** oder über die Menüleiste **Debuggen/Debuggen starten**
6. Ihre Website sollte nun wie folgt aussehen:

![](_images/posts-on-start.png?raw=true "Abbildung 1")

7. Beenden Sie das Debugging **nicht**
8. Wechseln Sie zu **Visual Studio**
9. Öffnen Sie die Datei **Views/Home/Index.cshtml**
10. Umhüllen Sie das Element, das das **Bild** eines Posts (**\<img /\>**) darstellt, mit einen Link (**\<a\>... \</a\>**), der das Bild im Großformat anzeigt

	```XML
    <a href="~/Uploads/@item.Image.FileName"><img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" style="vertical-align:middle" /></a>
	```
	
11. Speichern Sie Ihre Änderungen
12. Wechseln Sie in Ihren Browser
13. **Aktualisieren** Sie die Seite
14. Klicken Sie auf eines der Bilder:

![](_images/image.png?raw=true "Abbildung 2")

15. Beenden Sie das Debugging

## Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie man einen DbInitializer verwendet, um die Datenbank zu Beginn mit Daten zu füllen
- Was der DbContext tut
- Wie Eigenschaften einer Entität deklariert werden müssen
- Wie Sie die Aufgabenliste von Visual Studio verwenden
- Wie eine Datei aus einem HttpRequest gespeichert werden kann
- Wie man einem Bild ein Overlay hinzugefügt
- Wie man LINQ verwendet
- Wie Sie Razor-Syntax verwenden, um auf Eigenschaften eines ViewModels zuzugreifen
- Wie Sie Html-Syntax verwenden, um Daten aus einem ViewModel anzuzeigen
- Dass Sie Änderungen am Code auch vornehmen können, während sich die Anwendung im Debugging befindet

