# Modul 5 - Entwicklung einer Schnittstelle mit ASP.NET Web API: ASP.NET Web API einrichten

## Übersicht 

In diesem Modul lernen Sie die Grundlagen zu ASP.NET Web API kennen.

TODO: Ziele dieses Modus
TODO: Ankersprünge zu Übungen
TODO: Umstellung auf JSON in WebApiConfig
TODO: Doku über /Help Page

## Präsentation

Sehen Sie sich die zu dem Modul gehörende [Präsentation](05. Entwicklung einer Schnittstelle mit ASP.NET Web API) an.

## Ziele

In diesem Hands-On lernen Sie:
- Wie Sie Routen verwenden, um gezielt auf Apis zu verweisen  
- Wie Sie ein Entityset sortieren  
- Wie Sie einer Api-Methode über Attribute mitteilen, welchen Typ sie zurückgibt  
- Wie man mit Hilfe von Postman eine Api schnell testen kann  

---

## Übungen

1. ApiControllers erstellen und vervollständigen
2. Api testen mit Postman

### Übung 1 - ApiControllers erstellen und vervollständigen

#### Aufgabe 1 - Dto-Klassen hinzufügen

1.	Erzeugen Sie einen neuen Ordner **Dtos** unterhalb des Ordners **Models**. Sie können das über einen Rechtsklick auf den Ordner im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner Models/Dtos und wählen **Hinzufügen/Vorhandenes Element**.
3.	Im Dialogfeld navigieren Sie in den Ordner **Models/Dtos** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4.	Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer.png?raw=true "Abbildung 1")

#### Aufgabe 2 - ApiController vorbereiten

1.	Erzeugen Sie einen neuen Ordner **ApiControllers** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner **ApiControllers** und wählen **Hinzufügen/Vorhandenes Element**.
3.	Im Dialogfeld navigieren Sie in den Ordner **Dateien/ApiControllers** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4.	Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer-2.png?raw=true "Abbildung 2")

#### Aufgabe 3 - ApiController um Businesslogik erweitern

1.	Öffnen Sie die Datei **ApiControllers/PostsController.cs**
2.	Fügen Sie eine Methode hinzu, die über die Route **api/posts/popular** erreichbar ist und die 10 Posts mit der höchsten Beliebtheit in absteigender Reihenfolge zurückgibt

    ```C#
		// GET api/posts/popular
		[Route("api/posts/popular")]
		public IEnumerable<PostDto> GetPopular()
		{
			return this.db.Posts.OrderByDescending(o => o.Likes.Count).Take(10).ToList().Select(p => PostDto.Map(p));
		}
    ```
	
3.	Öffnen Sie die Datei **ApiControllers/LikesController**
4.	Ersetzen Sie den Code der Methode **PostLike** mit folgendem:

    ```C#
        public IHttpActionResult PostLike(LikeDto likeDto)
        {
            // Get user
            var user = db.Users.FirstOrDefault(u => u.Identifier == likeDto.UserIdentifier);
            if (user == null)
            {
                return BadRequest("Invalid user");
            }

            // Get post
            var post = db.Posts.Find(likeDto.PostId);
            if (post == null)
            {
                return BadRequest("Invalid post");
            }

            var like = db.Likes.FirstOrDefault(l => l.User.Identifier == likeDto.UserIdentifier && l.Post.Id == likeDto.PostId);

            if (like != null)
            {
                // Remove the like and return
                db.Likes.Remove(like);
                db.SaveChanges();

                return Ok(like);
            }
            else
            {
                like = new Like
                {
                    User = user,
                    Post = post
                };

                // Add like
                db.Likes.Add(like);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = like.Id }, likeDto);
            }
        }
    ```
	
5.	Teilen Sie der Methode **PostLike** über das Attribut **ResponseType** mit, dass es ein Objekt vom Typ **Like** zurückgibt

    ```C#
        // POST: api/likes
        [ResponseType(typeof(Like))]
        public IHttpActionResult PostLike(LikeDto likeDto)
    ```

6.	Öffnen Sie die Datei **ApiControllers/UsersController.cs**
7.	Ersetzen Sie den Code der Methode **GetUser** mit folgendem:

    ```C#
        public IHttpActionResult GetUser(string id)
        {
            var user = db.Users.FirstOrDefault(u => u.Identifier == id);
            if (user == null)
            {
                return NotFound();
            }

            // Map found user to dto
            var result = UserDto.Map(user);
            return Ok(result);
        }
    ```
	
8.	Teilen Sie der Methode **GetUser** über das Attribut **ResponseType** mit, dass es ein Objekt vom Typ **UserDto** zurückgibt

    ```C#
        [ResponseType(typeof(UserDto))]
    ```
    
    
### Übung 2 - APIs Testen und Dokumentieren

#### Aufgabe 1 - API testen mit Postman

1.	Öffnen Sie den Chrome Browser, falls installiert
2.	Navigierien Sie zum Chrome Web Store

https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop

![](_images/postman-1.png?raw=true "Abbildung 3")

3.	Installieren Sie sich die App **Postman**
4.	Öffnen Sie Chrome.
5.	Geben Sie oben in die Adressleiste **chrome://apps** ein.
6.	Drücken Sie die Eingabetaste.
7.	Öffnen Sie Postman

![](_images/postman-2.png?raw=true "Abbildung 4")

8.	Starten Sie Ihre Anwendung aus Visual Studio heraus und speichern Sie sich die Url zu dieser in der Zwischenablage (z.B. http://localhost:60783/)
9.	Wechseln Sie zum Chrome Browser
10.	In der Zeile **Enter request URL here** fügen Sie die Url zu Ihrer Anwendung und erweitern diese um die Api, die Sie testen wollen 
http://localhost:60783/api/posts/popular
11.	Drücken Sie auf die Schaltfläche **Send**

![](_images/postman-3.png?raw=true "Abbildung 5")

12.	Prüfen Sie, ob die Daten angezeigt werden, die Sie erwartet haben

#### Aufgabe 2 - API testen mit ASP.NET Web Test Client
Installation
http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/creating-api-help-pages

Testing
Install-Package WebApiTestClient

#### Aufgabe 3 - API dokumentieren mit Swagger
Install-Package Swashbuckle -Version 5.2.1 

http://localhost:XXXX/swagger

## Zusammenfassung

In diesem Hands-On haben Sie gelernt:
- Wie Sie Routen verwenden, um gezielt auf Apis zu verweisen  
- Wie Sie ein Entityset sortieren  
- Wie Sie einer Api-Methode über Attribute mitteilen, welchen Typ sie zurückgibt  
- Wie man mit Hilfe von Postman eine Api schnell testen kann  
