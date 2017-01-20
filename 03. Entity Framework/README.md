# Modul 3 - Datenmodellierung und -abfrage mit dem Entity Framework

## Übersicht 

In diesem Modul lernen Sie die Grundlagen zum Entity Framework kennen.

## Präsentation

Sehen Sie sich die [Präsentation](Entity Framework.pptx) zu diesem Modul an.

## Ziele

- Das Entity Framework kennenlernen
- Datenzugriffsmethoden kennenlernen
- Die Razor Syntax und View Rendering kennenlernen

---

## Übungen

Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Erstellen des Datenmodells</a><br/>
2. <a href="#Exercise2">Datenzugriff</a><br />
3. <a href="#Exercise3">Datenanzeige mit der Razor-Syntax</a>

<a name="Exercise1"></a>
### Übung 1: Erstellen des Datenmodells

In dieser Übung werden wir:
- Das NuGet Paket **EntityFramework** installieren
- Das Datenmodell mit Code First erstellen
- Eigenschaften zu Entitäten hinzufügen
- Testdaten erzeugen
- Eine Verbindung zur LocalDB herstellen

![](_images/dbmodel.png?raw=true "Abbildung 1")
Abbildung 1: Das fertige Datenmodell

#### Aufgabe 1 - NuGet-Paket EntityFramework installieren
In dieser Aufgabe wird das NuGet-Paket **EntityFramework** installiert.

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **DotNETJumpStart** und wählen **NuGet-Pakete verwalten...**".<br/><br/>
   ![](_images/manage-nuget-packages.png?raw=true "Abbildung 2")
3. Im Paketmanager, unter dem Reiter "**Durchsuchen**", wählen Sie links das Paket **EntityFramework** und klicken anschließend rechts auf **Installieren**<br/><br/>
   ![](_images/NuGet-EntityFramework.png?raw=true "Abbildung 3")

Nach einem Moment ist das Paket installiert und Sie sind bereit mit der Entwicklung zu beginnen.  

#### Aufgabe 2 - Hinzufügen der Entitäten

1. Machen Sie einen Rechtsklick im **Projektmappen-Explorer** auf den Ordner **Models** und wählen **Hinzufügen/Vorhandenes Element**
2. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Models** aus dem aktuellen Hands-On und wählen alle Dateien aus.
3. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/start.png?raw=true "Abbildung 4")

#### Aufgabe 3 - Hinzufügen weiterer Eigenschaften

1. Öffnen Sie die Aufgabenliste über die Menüleiste **Ansicht/Aufgabenleiste** oder drücken Sie die Tasten **STRG+W, T**
2. Das Aufgabenfenster sollte wie folgt aussehen:

  ![](_images/todos.png?raw=true "Abbildung 5")

3. Doppelklicken Sie auf den ersten Eintrag oder öffnen Sie die Datei **Models/User.cs**

  ![](_images/user-entity.png?raw=true "Abbildung 6")

4. Fügen Sie der Klasse User die Eigenschaft **Name** vom Typ **String** mit den Attributen **Required** und **MaxLength = 50** hinzu. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft „Identifier“.
5. Fügen Sie der Klasse User die Eigenschaft **Posts** hinzu. Diese Eigenschaft soll alle vom Benutzer geposteten Einträge enthalten. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft „Likes“.	
6. Erstellen Sie die Projektmappe über die Menüleiste **Erstellen/Projektmappe neu erstellen** und stellen Sie sicher, dass keine Fehler auftreten.
	

#### Aufgabe 4 - DbContext bearbeiten 

1. Erzeugen Sie einen neuen Ordner **DataContext** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **DataContext** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/DataContext** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen: <br/><br/>
  ![](_images/dbcontext.png?raw=true "Abbildung 7")

5. Öffnen Sie in der Aufgabenliste die erste Aufgabe oder öffnen Sie die Datei **ImageAppDbContext.cs**
6. Fügen Sie der **ImageAppDbContext** Klasse 3 DbSets für die Entitäten **User**, **Post** und **Like** hinzu. Orientieren Sie sich dabei an der bestehen Eigenschaft **Images**. Über ein DbSet können Sie auf die Datenbankeinträge einer bestimmten Entität zugreifen (wie z.B. Posts).
7. Öffnen Sie die Datei **ImageAppDbInitializer.cs**
8. In dieser Datei können Sie die Datenbank mit Beispieldaten initialisieren. Ersetzen Sie den Rumpf der Methode **Seed** dazu mit folgendem Inhalt:

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

        // Create some images
        var images = new List<Image>
        {
            new Image { FileName = "business-q-c-1024-768-9.jpg" },
            new Image { FileName = "cats-q-c-1024-768-4.jpg" },
            new Image { FileName = "city-q-c-1024-768-9.jpg" },
            new Image { FileName = "sports-q-c-1024-768-4.jpg" },
            new Image { FileName = "technics-q-c-1024-768-4.jpg" }
        };

        images.ForEach(s => context.Images.Add(s));
        context.SaveChanges();

        // Create some posts
        var posts = new List<Post>
        {
            new Post { Title = "Business stuff", User = users.First(), Image = images.First() },
            new Post { Title = "My cat", User = users.First(), Image = images.Skip(1).First() },
            new Post { Title = "Random bridge", User = users.Skip(1).First(), Image = images.Skip(2).First() },
            new Post { Title = "Surfin' U.S.A.", User = users.Skip(1).First(), Image = images.Skip(3).First() },
            new Post { Title = "Technics", User = users.First(), Image = images.Skip(4).First() }
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
	
9. Erstellen Sie die Projektmappe über die Menüleiste **Erstellen/Projektmappe neu erstellen** und stellen Sie sicher, dass keine Fehler auftreten.

#### Aufgabe 5 - Testbilder bereitstellen
1. Erzeugen Sie einen neuen Ordner **Uploads** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Uploads** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Uploads** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/uploads.png?raw=true "Abbildung 8")

#### Aufgabe 6 - DbContext und DbInitializer bekannt machen 
1. Öffnen Sie die Datei **Web.config** im **Stammverzeichnis** Ihrer Projektmappe
2. Fügen Sie den folgenden am Ende der Datei direkt vor dem schließenden **</configuration>**-Tag ein.

    ```XML
      <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\app-db.mdf;Integrated Security=True;" providerName="System.Data.SqlClient" />
      </connectionStrings>
    ```
    
3. Ersetzen Sie die Konfigurationssektion **entityFramework** durch folgenden Inhalt

	```XML
    <entityFramework>
        <contexts>
          <context type="DotNETJumpStart.Models.ImageAppDbContext, DotNETJumpStart">
            <databaseInitializer type="DotNETJumpStart.Models.ImageAppDbInitializer, DotNETJumpStart" />
          </context>
        </contexts>
        <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" />
        <providers>
          <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
        </providers>
    </entityFramework>
	```
	
4. Speichern Sie Ihre Änderungen und erstellen die Projektmappe neu.

Durch diese Änderung haben Sie sichergestellt, das 1.) Die Datenbank in einer lokalen Datei (app-db.mdf) erzeugt wird, falls sie noch nicht vorhanden ist und B) Die Testdaten aus dem DbInitializer vor dem ersten Datenzugriff erstellt werden.

<a name="Exercise2"></a>
### Übung 2: Datenzugriff

In dieser Übung werden wir:
- Ein Bild aus einem HttpRequest speichern
- Einem Bild ein Wasserzeichen hinzufügen
- Daten festlegen, die in einer View angezeigt werden sollen

#### Aufgabe 1 - Hinzufügen der ImageUtils und Bearbeiten der Controller

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter.
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
	using DotNETJumpStart.Models;
    ```
	
3. Ändern Sie die Methode **Index()** so ab, dass Sie einen Datenbankzugriff macht und die 10 neuesten Posts ausliest.

    ```C#
	private ImageAppDbContext db = new ImageAppDbContext();

	public ActionResult Index()
	{
		var posts = db.Posts.OrderByDescending(p => p.Created).Take(10);
		return View(posts.ToList());
	}
	```
	
4. Speichern und schließen Sie die Datei

#### Aufgabe 3 - Anwendung starten
1. Starten Sie Ihre Anwendung über die Menüleiste **Debuggen/Debugging starten** oder das Tastenkürzel **F5**
2. Durch den Zugriff auf den **ImageAppDbContext** im **HomeController** wird die Datenbank automatisch generiert und Testdaten werden eingefügt
3. Beenden Sie das Debugging durch das Schließen des Browsers oder über die Menüleiste **Debugging/Debugging beenden** innerhalb von Visual Studio

#### Aufgabe 4 - Tabelldaten anzeigen
1. Öffnen Sie den **Server-Explorer**. Am einfachsten geben Sie **Server-Explorer** oben rechts in die Suche ein oder drücken Strg+W+L auf der Tastatur.
2. Suchen Sie die zuvor hinzugefügte Verbindung zur lokalen Datenbank und klappen Sie sie auf (unter Datenverbindungen / DefaultConnection / Tabellen)
3. Ihr Server-Explorer sollte nun die Tabellen der einzelnen Entitäten (Image, Like, Post und User enthalten):

![](_images/table-view.png?raw=true "Abbildung 9")

5. Machen Sie einen Rechtsklick auf die Tabelle **Post** und wählen den Menüeintrag **Tabellendaten anzeigen**
6. Ihre Projektmappe sollte nun wie folgt aussehen

![](_images/data-view.png?raw=true "Abbildung 10")

<a name="Exercise3"></a>
### Übung 3: Datenanzeige mit der Razor-Syntax

In dieser Übung werden wir:
- Die Razor-Syntax im HTML verwenden, um auf Daten des Models zuzugreifen und diese auf der View anzuzeigen.

#### Aufgabe 1 - Posts auf der Startseite anzeigen
1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter.
2. Öffnen Sie die Datei **Views/Home/Index.cshtml**
3. Um die Daten aus der zuvor angepassten **Index**-Methode des **HomeController**s anzuzeigen, ersetzen Sie den Inhalt der Datei mit folgendem Code:

	```HTML
	@model IEnumerable<DotNETJumpStart.Models.Post>
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
	
4. Speichern Sie die Datei und machen Sie sich mit der Struktur vertraut. Betrachten Sie, wie die vom HomeController zurückgegebene Liste mit Post-Einträgen in der View verwendet wird.
5. Starten Sie Ihre Anwendung über das Tastenkürzel **F5** oder über die Menüleiste **Debuggen/Debuggen starten**
6. Ihre Website sollte nun wie folgt aussehen:

![](_images/posts-on-start.png?raw=true "Abbildung 1")

#### Aufgabe 2 - Bilder in voller Größe anzeigen
1. Beenden Sie das Debugging **nicht**
2. Wechseln Sie zu **Visual Studio**
3. Öffnen Sie die Datei **Views/Home/Index.cshtml**
4. Umhüllen Sie das Element, das das **Bild** eines Posts (**\<img /\>**) darstellt, mit einen Link (**\<a\>... \</a\>**), der das Bild im Großformat anzeigt

	```HTML
    <a href="~/Uploads/@item.Image.FileName"><img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" style="vertical-align:middle" /></a>
	```
	
5. Speichern Sie Ihre Änderungen
6. Wechseln Sie in Ihren Browser
7. **Aktualisieren** Sie die Seite
8. Klicken Sie auf eines der Bilder:

  ![](_images/image.png?raw=true "Abbildung 2")  


9. Beenden Sie das Debugging

## Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie man einen DbInitializer verwendet, um die Datenbank zu Beginn mit Daten zu füllen
- Was der DbContext tut
- Wie Eigenschaften einer Entität deklariert werden müssen
- Wie Sie die Aufgabenliste von Visual Studio verwenden  
- Wie man einem Bild ein Wasserzeichen hinzufügt
- Wie man LINQ verwendet
- Wie Sie Razor-Syntax verwenden, um auf Eigenschaften eines ViewModels zuzugreifen
- Wie Sie HTML-Syntax verwenden, um Daten aus einem ViewModel anzuzeigen
- Dass Sie Änderungen am Code auch vornehmen können, während sich die Anwendung im Debugging befindet