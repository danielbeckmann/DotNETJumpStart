Modul 3/01 - Datenmodellierung und -abfrage mit dem Entity Framework: Erstellen des Datenmodells
=======================================

##Übersicht 

##Ziele
- Das fertige Datenmodell

![](images/dbmodel.png?raw=true "Abbildung 1")

- Hinzufügen von Eigenschaften zu Entitäten
- Erzeugen von Testdaten
- Herstellen der Verbindung zur LocalDB

##Übungen - Datenmodellierung

###Aufgabe 1 - Hinzufügen der Entitäten

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Models** und wählen **Hinzufügen/Vorhandenes** Element
3. Im Dialogfeld navigieren Sie in den Ordner **Files/Models** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/2.png?raw=true "Abbildung 2")

###Aufgabe 2 - Hinzufügen weiterer Eigenschaften

1. Öffnen Sie die Aufgabenliste über die Menüleiste **Ansicht/Aufgabenleiste** oder drücken Sie die Tasten **STRG+W, T**
2. Das Aufgabenfenster sollte wie folgt aussehen:

![](images/3.png?raw=true "Abbildung 3")

3. Doppelklicken Sie auf den ersten Eintrag oder öffnen Sie die Datei **Models/User.cs**

![](images/4.png?raw=true "Abbildung 4")

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
	
###Aufgabe 3 - DbContext bearbeiten 

1. Erzeugen Sie einen neuen Ordner **DataContext** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **DataContext** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Files/DataContext** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/5.png?raw=true "Abbildung 5")

5. Öffnen Sie in der Aufgabenliste die erste Aufgabe oder öffnen Sie die Datei **ImageAppDbContext.cs**
6. Fügen Sie der **ImageAppDbContext** Klasse die Set-Eigenschaften für die Entitäten **User**, **Post** und **Like** hinzu. Orientieren Sie sich dabei an der bestehen Eigenschaft zur Entität **Images**

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
	
9. Erstellen Sie die Projektmappe über die Menüleiste **Erstellen/Projektmappe** neu erstellen und stellen Sie sicher, dass keine Fehler auftreten.

###Aufgabe 4 - Testbilder bereitstellen
1. Erzeugen Sie einen neuen Ordner **Uploads** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Uploads** und wählen **Hinzufügen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Files/Uploads** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/6.png?raw=true "Abbildung 6")

###Aufgabe 5 - DbContext und DbInitializer bekannt machen 
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

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie man einen DbInitializer verwendet, um die Datenbank zu Beginn mit Daten zu füllen
- Was der DbContext tut
- Wie Eigenschaften einer Entität deklariert werden müssen
- Wie Sie die Aufgabenliste von Visual Studio verwenden