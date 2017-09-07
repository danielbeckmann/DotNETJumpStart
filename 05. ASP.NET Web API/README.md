# Modul 5 - Entwicklung einer Schnittstelle mit ASP.NET Web API

## Übersicht 

In diesem Modul lernen Sie die Grundlagen zum ASP.NET Web API-Framework kennen.
Hierzu werden Sie eine Schnittstelle implementieren, über die eine App Posts abrufen und erstellen kann.

## Präsentation

Sehen Sie sich die [Präsentation](Entwicklung%20einer%20Schnittstelle%20mit%20ASP.NET%20Web%20API.pptx) zu diesem Modul an.

## Ziele

In diesem Hands-On lernen Sie:
- Wie man Web API Controller einsetzt, um Daten über eine Schnittstelle bereitzustellen
- Wie Sie DTO-Klassen für den Datenaustausch verwenden
- Wie Sie JSON als Datenformat konfigurieren
- Wie man eine API dokumentieren kann

---

## Übungen

1. <a href="#Exercise1">Erstellen und Vervollständigen der API Controller</a>
2. <a href="#Exercise2">Dokumentieren der API</a>

<a name="Exercise1"></a>

### Übung 1 - Erstellen und Vervollständigen der API Controller

#### Aufgabe 1 - Dto-Klassen hinzufügen

APIs verwenden üblicherweise ein einfacheres Datenmodell, als das was für den Datenbankzugriff verwendet wird. Diese Datenklassen werden als DTOs (Data-Transfer-Objekte) bezeichnet. Diese Datenklassen wurden bereits für Sie vorbereitet und müssen noch hinzugefügt werden:

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2.	Erzeugen Sie einen neuen Ordner mit dem Namen "**Dtos**" unterhalb des Ordners "**Models**". Sie können das über einen Rechtsklick auf den Ordner im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
3.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner "**Models/Dtos**" und wählen **Hinzufügen/Vorhandenes Element**.
4.	Im Dialogfeld navigieren Sie in den Ordner **Models/Dtos** aus dem aktuellen Hands-On und wählen alle Dateien aus.
5.	Die Projektmappe sollte nun wie folgt aussehen:

 ![](_images/solution-explorer.png?raw=true "Abbildung 1")

6. Vergleichen Sie den Inhalt der einzelnen **Dtos** mit den Modelklassen, die von der Webanwendung für den Datenzugriff verwendet werden.


#### Aufgabe 2 - ApiController vorbereiten

Die Endpunkte der Schnittstelle werden in einzelnen Controllern definiert. Hierzu wurden bereits ein paar Controller für Sie vorbereitet:

1.	Erzeugen Sie einen neuen Ordner "**ApiControllers**" im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner "**ApiControllers**" und wählen **Hinzufügen/Vorhandenes Element**.
3.	Im Dialogfeld navigieren Sie in den Ordner **Dateien/ApiControllers** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4.	Die Projektmappe sollte nun wie folgt aussehen:

 ![](_images/solution-explorer-2.png?raw=true "Abbildung 2")

5. Öffnen Sie die Datei **PostsController.cs** und machen sich mit dem Code der Methode **Get** vertraut. Diese wird automatisch aufgerufen, wenn ein Zugriff über den URL-Pfad "api/posts" erfolgt.
5. Starten Sie mit **F5** die Webanwendung. Durch die hinzugefügten ApiController wird die API automatisch gestartet.
6. Im sich öffnenden Browserfenster hängen Sie folgenden Pfad an die aktuelle URL an: "**/api/posts**". Sie sehen daraufhin die Ausgabe der Posts im Browserfenster in XML-Darstellung:

 ![](_images/xml-output-browser.png?raw=true "Abbildung 2")

7. Schließen Sie das Browserfenster und beenden Sie das aktive Debugging mit Umschalt+F5.

#### Aufgabe 3 - JSON als Datenformat konfigurieren

Heutzutage wird in den meisten Fällen das Datenformat **JSON** verwendet und nicht mehr XML. Dies lässt sich auch für Web API schnell konfigurieren:

1. Öffnen Sie die Datei **App_Start/WebApiConfig.cs**.
2. Fügen Sie folgende Zeilen ans Ende der Methode **Register** hinzu:

    ```C#
    // Remove xml serializer
    config.Formatters.Remove(config.Formatters.XmlFormatter);

    // Ignore EF object loop references
    GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌ferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;    
	```

3. Starten Sie die Webanwendung und rufen "**/api/posts**" erneut im Browser auf. Die Ausgabe sollte nun in JSON erfolgen:

  ![](_images/json-output-browser.png?raw=true "Abbildung 2")

4. Beenden Sie das Debugging.

#### Aufgabe 4 - PostsController mit Businesslogik erweitern

1. Öffnen Sie die Datei **ApiControllers/PostsController.cs**
2. Fügen Sie dem Controller die nachfolgende Methode hinzu und machen sich mit der Implementierung vertraut. Diese ist über die feste Route **api/posts/latest** erreichbar und gibt alle Posts, absteigend nach dem Erstellungsdatum sortiert, zurück.

    ```C#
        // GET api/posts/latest
        [Route("api/posts/latest")]
        public IEnumerable<PostDto> GetLatest()
        {
            return this.db.Posts.OrderByDescending(o => o.Likes).ToList().Select(p => PostDto.Map(p));
        }
    ```
	
3. Erweitern Sie den Controller nun um eine Methode **GetPopular**, die  über die Route **api/posts/popular** erreichbar ist und die Posts nach der höchsten Beliebtheit in absteigender Reihenfolge zurückgibt (Die Lösung finden Sie im aktuellen Verzeichnis im Ordner **Quellcode** und der jeweiligen Datei).

    ```C#
        // GET api/posts/popular
        // TODO: Define the route here (api/posts/latest)
        public IEnumerable<PostDto> GetPopular()
        {
            // TODO: Return the posts, ordered by popularity (by Likes.Count)
        }
    ```

4. Machen Sie sich mit der restlichen Implementierung des Controllers vertraut. Interessant ist beispielsweise die Methode **PostPost**, die einen neuen Post erzeugt.

#### Aufgabe 5 - LikesController mit Businesslogik erweitern

1. Öffnen Sie die Datei **ApiControllers/LikesController**
2. Machen Sie sich mit dem Code der Methode **PostLike** vertraut, die einem Post einen Like hinzufügt, bzw. das Like entfernt, wenn es bereits vorhanden ist.
3. Erweitern Sie die Methode **PostLike** um den notwendigen Code, um dem Post einen Like hinzuzufügen, wenn noch keine Like vom aktuellen Benutzer vorhanden ist. (Die Lösung finden Sie im aktuellen Verzeichnis im Ordner **Quellcode** und der jeweiligen Datei).

    ```C#
        // ...
        
        var like = db.Likes.FirstOrDefault(l => l.User.Identifier == likeDto.UserIdentifier && l.Post.Id == likeDto.PostId);

        if (like != null)
        {
            // Remove the like and return
            db.Likes.Remove(like);

            db.SaveChanges();
            return Ok();
        }
        else
        {
            // TODO: Create a new like for the current post and the current user here 

            // TODO: Add the like to the Likes table here

            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = like.Id }, likeDto);
        }

        // ...
    ```
<a name="Exercise2"></a>

### Übung 2 - Dokumentieren der API

#### Aufgabe 1 - Help Pages betrachten

In Ihrer Projektmappe wird vom Framework bereits automatisch eine Dokumentationsseite aus Ihren API Controllern erzeugt.

1. Starten Sie die Webanwendung und wählen in der Navigation den Punkt **API**.
2. Betrachten Sie die automatisch erzeugte Dokumentation.
3. Beenden Sie das Debugging.

#### Aufgabe 2 - Quellcode Kommentare hinzufügen

Um Ihre Quellcode-Kommentare ebenfalls in der Dokumentation anzuzeigen, muss die Ausgabe der **XML-Dokumentation** aktiviert werden.

1. Öffnen Sie hierzu die Datei **Areas/HelpPage/App_Start/HelpPageConfig.cs** und kommentieren Sie folgende Zeile in der Methode **Register** aus:
    
    ```C#
    config.SetDocumentationProvider(new XmlDocumentationProvider(
        HttpContext.Current.Server.MapPath("~/App_Data/XmlDocument.xml")));
    ```

2. Machen Sie nun einen Rechtsklick im Projektmappen-Explorer auf das Projekt **DotNETJumpStart** und öffnen die **Eigenschaften**.
3. Dort navigieren Sie zum Bereich **Erstellen**  

  ![](_images/project-settings.png?raw=true "Abbildung 3")

3. Dort aktivieren Sie die Funktion **XML-Dokumentationsdatei** und tragen folgenden Dateinamen ein: "**App_Data/XmlDocument.xml**".

 ![](_images/project-settings-doc.png?raw=true "Abbildung 3")

4. Öffnen Sie die Datei **ApiControllers/PostsController** und fügen für ein paar Funktionen eine XML-Dokumentation hinzu. Beispielsweise:

    ```C#
        /// <summary>
        /// Gibt alle Posts als Liste zurück.
        /// </summary>
        /// <returns>Liste aller Posts</returns>
        public IEnumerable<PostDto> Get()
        {
            return this.db.Posts.ToList().Select(p => PostDto.Map(p));
        }
    ```

5. Erstellen und starten Sie die Webanwendung und navigieren zur **API-Dokumentation**. Dort sollten nun die Kommentare angezeigt werden.

## Zusammenfassung

In diesem Hands-On haben Sie gelernt:
- Wie man Web API Controller einsetzt, um Daten über eine Schnittstelle bereitzustellen
- Wie Sie DTO-Klassen für den Datenaustausch verwenden
- Wie Sie JSON als Datenformat konfigurieren
- Wie man eine API dokumentieren kann