Modul 5/01 - Entwicklung einer Schnittstelle mit ASP.NET Web API: ASP.NET Web API einrichten
=======================================

##Ziele
In diesem Hands-On lernen Sie
-Wie Sie Routen verwenden, um gezielt auf Apis zu verweisen
-Wie Sie ein Entityset sortieren
-Wie Sie einer Api-Methode �ber Attribute mitteilen, welchen Typ sie zur�ckgibt
-Wie man mit Hilfe von Postman eine Api schnell testen kann

###�bung 1 - ApiControllers erstellen und vervollst�ndigen

###Aufgabe 1 - Dto-Klassen hinzuf�gen

1.	Erzeugen Sie einen neuen Ordner **Dtos** unterhalb des Ordners **Models**. Sie k�nnen das �ber einen Rechtsklick auf den Ordner im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner Models/Dtos und w�hlen **Hinzuf�gen/Vorhandenes Element**.
3.	Im Dialogfeld navigieren Sie in den Ordner **Models/Dtos** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4.	Die Projektmappe sollte nun wie folgt aussehen:

![](images/1.png?raw=true "Abbildung 1")

###Aufgabe 2 - ApiController vorbereiten

1.	Erzeugen Sie einen neuen Ordner **ApiControllers** im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2.	Machen Sie einen Rechtsklick auf den neu erstellten Ordner **ApiControllers** und w�hlen **Hinzuf�gen/Vorhandenes Element**.
3.	Im Dialogfeld navigieren Sie in den Ordner **Files/ApiControllers** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4.	Die Projektmappe sollte nun wie folgt aussehen:

![](images/2.png?raw=true "Abbildung 2")

###Aufgabe 3 - ApiController um Businesslogik erweitern

1.	�ffnen Sie die Datei **ApiControllers/PostsController.cs**
2.	F�gen Sie eine Methode hinzu, die �ber die Route **api/posts/popular** erreichbar ist und die 10 Posts mit der h�chsten Beliebtheit in absteigender Reihenfolge zur�ckgibt

    ```C#
		// GET api/posts/popular
		[Route("api/posts/popular")]
		public IEnumerable<PostDto> GetPopular()
		{
			return this.db.Posts.OrderByDescending(o => o.Likes.Count).Take(10).ToList().Select(p => PostDto.Map(p));
		}
    ```
	
3.	�ffnen Sie die Datei **ApiControllers/LikesController**
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
	
5.	Teilen Sie der Methode **PostLike** �ber das Attribut **ResponseType** mit, dass es ein Objekt vom Typ **Like** zur�ckgibt

    ```C#
        // POST: api/likes
        [ResponseType(typeof(Like))]
        public IHttpActionResult PostLike(LikeDto likeDto)
    ```

6.	�ffnen Sie die Datei **ApiControllers/UsersController.cs**
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
	
8.	Teilen Sie der Methode **GetUser** �ber das Attribut **ResponseType** mit, dass es ein Objekt vom Typ **UserDto** zur�ckgibt

    ```C#
        [ResponseType(typeof(UserDto))]
    ```

##�bung 2 (Optional) - Api testen mit Postman

1.	�ffnen Sie den Chrome Browser, falls installiert
2.	Navigierien Sie zum Chrome Web Store

https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop

![](images/3.png?raw=true "Abbildung 3")

3.	Installieren Sie sich die App **Postman**
4.	�ffnen Sie Chrome.
5.	Geben Sie oben in die Adressleiste **chrome://apps** ein.
6.	Dr�cken Sie die Eingabetaste.
7.	�ffnen Sie Postman

![](images/4.png?raw=true "Abbildung 4")

8.	Starten Sie Ihre Anwendung aus Visual Studio heraus und speichern Sie sich die Url zu dieser in der Zwischenablage (z.B. http://localhost:60783/)
9.	Wechseln Sie zum Chrome Browser
10.	In der Zeile **Enter request URL here** f�gen Sie die Url zu Ihrer Anwendung und erweitern diese um die Api, die Sie testen wollen 
http://localhost:60783/api/posts/popular
11.	Dr�cken Sie auf die Schaltfl�che **Send**

![](images/5.png?raw=true "Abbildung 5")

12.	Pr�fen Sie, ob die Daten angezeigt werden, die Sie erwartet haben

##Zusammenfassung
In diesem Hands-On haben Sie gelernt
-Wie Sie Routen verwenden, um gezielt auf Apis zu verweisen
-Wie Sie ein Entityset sortieren
-Wie Sie einer Api-Methode �ber Attribute mitteilen, welchen Typ sie zur�ckgibt
-Wie man mit Hilfe von Postman eine Api schnell testen kann