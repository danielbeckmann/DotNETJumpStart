Modul 3/01 - Datenmodellierung und -abfrage mit dem Entity Framework: Erstellen des Datenmodells
=======================================

##�bersicht 

##Ziele
- Das fertige Datenmodell

![](images/dbmodel.png?raw=true "Abbildung 1")

- Hinzuf�gen von Eigenschaften zu Entit�ten
- Erzeugen von Testdaten
- Herstellen der Verbindung zur LocalDB

##�bungen - Datenmodellierung

###Aufgabe 1 - Hinzuf�gen der Entit�ten

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder �ffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Models** und w�hlen **Hinzuf�gen/Vorhandenes** Element
3. Im Dialogfeld navigieren Sie in den Ordner **Files/Models** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/2.png?raw=true "Abbildung 2")

###Aufgabe 2 - Hinzuf�gen weiterer Eigenschaften

1. �ffnen Sie die Aufgabenliste �ber die Men�leiste **Ansicht/Aufgabenleiste** oder dr�cken Sie die Tasten **STRG+W, T**
2. Das Aufgabenfenster sollte wie folgt aussehen:

![](images/3.png?raw=true "Abbildung 3")

3. Doppelklicken Sie auf den ersten Eintrag oder �ffnen Sie die Datei **Models/User.cs**

![](images/4.png?raw=true "Abbildung 4")

4. F�gen Sie der Klasse User die Eigenschaft **Name** vom Typ **String** mit den Attributen **Required**, **MaxLength = 50** und **Unique** hinzu. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft �Identifier�.

    ```C#
    /// <summary>
    /// Gets or sets the users name
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Index(IsUnique = true)]
    public string Name { get; set; }
    ```
	
5. F�gen Sie der Klasse User die Eigenschaft **Posts** hinzu. Diese Eigenschaft stellt eine Eigenschaft aller vom Benutzer geposteten Eintr�ge dar. Orientieren Sie sich dabei an der bereits vorhandenen Eigenschaft �Likes�.	

	```C#
    /// <summary>
    /// Gets or sets the users posts.
    /// </summary>
    public virtual ICollection<Post> Posts { get; set; }
	```
	
6. Erstellen Sie die Projektmappe �ber die Men�leiste **Erstellen/Projektmappe** neu erstellen und stellen Sie sicher, dass keine Fehler auftreten.
	
###Aufgabe 3 - DbContext bearbeiten 

1. Erzeugen Sie einen neuen Ordner **DataContext** im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **DataContext** und w�hlen **Hinzuf�gen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Files/DataContext** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/5.png?raw=true "Abbildung 5")

5. �ffnen Sie in der Aufgabenliste die erste Aufgabe oder �ffnen Sie die Datei **ImageAppDbContext.cs**
6. F�gen Sie der **ImageAppDbContext** Klasse die Set-Eigenschaften f�r die Entit�ten **User**, **Post** und **Like** hinzu. Orientieren Sie sich dabei an der bestehen Eigenschaft zur Entit�t **Images**

	```C#
    public DbSet<Post> Posts { get; set; }

    public DbSet<Like> Likes { get; set; }

    public DbSet<User> Users { get; set; }
	```
	
7. �ffnen Sie die Datei **ImageAppDbInitializer.cs**
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
	
9. Erstellen Sie die Projektmappe �ber die Men�leiste **Erstellen/Projektmappe** neu erstellen und stellen Sie sicher, dass keine Fehler auftreten.

###Aufgabe 4 - Testbilder bereitstellen
1. Erzeugen Sie einen neuen Ordner **Uploads** im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Uploads** und w�hlen **Hinzuf�gen/Vorhandenes Element**.
3. Im Dialogfeld navigieren Sie in den Ordner **Files/Uploads** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/6.png?raw=true "Abbildung 6")

###Aufgabe 5 - DbContext und DbInitializer bekannt machen 
1. �ffnen Sie die Datei **Web.config** im **Stammverzeichnis** Ihrer Projektmappe
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
	
3. Speichern Sie Ihre �nderungen und erstellen die Projektmappe neu.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie man einen DbInitializer verwendet, um die Datenbank zu Beginn mit Daten zu f�llen
- Was der DbContext tut
- Wie Eigenschaften einer Entit�t deklariert werden m�ssen
- Wie Sie die Aufgabenliste von Visual Studio verwenden