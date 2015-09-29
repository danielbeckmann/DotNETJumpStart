Modul 4/01 - Aufbau einer Webapplikation mit ASP.NET MVC: Erstellen von Businesslogik und Views zum Verwalten der Posts-Entit�t
=======================================

##Ziele
In diesem Hands-On lernen Sie 
-Wie man Scaffolding verwendet, um Ansicht auf Basis einer Vorlage anzulegen
-Wie man Entitysets gezielt nach Eintr�gen filtert
-Wie man den Razor-Syntax verwendet, um Eigenschaften des zugrundeliegenden Datenmodells anzuzeigen
-Wie man �ber das Absenden eines Formulars nicht nur Zeichenketten und Zahlen, sondern auch Bilder �bermitteln kann
-Wie man die seiten�bergreifende Navigationsleiste bearbeiten kann
-Wie man CSS-Style zu einer Seite hinzuf�gt und dieses anwendet

##�bungen
Dieses Modul besteht aus zwei �bungen. In der ersten werden Businesslogik bereitgestellt sowie die Projektmappe f�r die gew�nschten Ansichten vorbereitet. In der zweiten �bung werden die durch das Scaffolding erzeugten Ansichten erweitert.

###�bung 1 - Bereitstellen der Businesslogik
###Aufgabe 1 - Erweitern des PostsController

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder �ffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Machen Sie einen Rechtsklick auf den Ordner **Controllers** und w�hlen **Hinzuf�gen/Vorhandenes Element**
3. Im Dialogfeld navigieren Sie in den Ordner **Files/Controllers** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](images/1.png?raw=true "Abbildung 1")

5. �ffnen Sie die Datei **PostsController.cs**
6. Ersetzen Sie den Code der Methode **Create** durch den folgendenen 

    ```C#
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            // Validate that a image was selected
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Ein Bild ist erforderlich");
            }

            if (ModelState.IsValid)
            {
                // Get image from request and save
                var image = ImageUtility.SaveImageFromRequest();

                // Save image to db
                image = db.Images.Add(image);
                db.SaveChanges();

                // Assign the image to the post
                post.Image = image;

                // Only the admin can post images here, so select admin
                post.User = db.Users.FirstOrDefault(x => x.Name == "Admin");

                // Save post to db
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(post);
        }
    ```

7. Ersetzen Sie den Code der Methode **DeleteConfirmed** durch folgenden

    ```C#
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            // Delete image from file system
            ImageUtility.DeleteImageFromDisk(post.Image);

            // Delete all referenced posts
            db.Likes.RemoveRange(post.Likes);

            db.Posts.Remove(post);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    ```

###Aufgabe 2 - Vorbereiten der Projektmappe f�r neue Ansichten

1. Erzeugen Sie einen neuen Ordner **Views/Posts** im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf den Ordner **Views** im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2. Die Projektmappe sollte nun wie folgt aussehen:
	
![](images/2.png?raw=true "Abbildung 2")

###Aufgabe 3 - Hinzuf�gen von Ansichten f�r das Bearbeiten, Editieren und L�schen von Posts

1. W�hlen Sie den Ordner **Views/Posts** an und f�gen ihm �ber einen Rechtsklick durch einen Klick auf die Schaltfl�che **Hinzuf�gen/Neues Ger�stelement** eine neue Ansicht hinzu
2. W�hlen Sie im aufgehenden Dialog die Vorlage **MVC5-Ansicht**

![](images/3.png?raw=true "Abbildung 3")

3. Ersellen Sie eine Ansicht unter der Vorlage **Create** mit dem Namen **Create** f�r die Modellklasse **Post** innerhalb der Datenkontextklasse **ImageAppDbContext**

![](images/4.png?raw=true "Abbildung 4")

4. Wiederholen Sie die Schritte **1** bis **3** f�r die Ansichten mit den Namen **Delete**, **Details** und **Edit** sowie deren korrespondierenden Vorlagen
5. Erstellen Sie eine Ansicht unter der Vorlage **List** mit dem Namen **Index** f�r die Modellklasse **Post** innerhalb der Datenkontextklasse **ImageAppDbContext**

![](images/5.png?raw=true "Abbildung 5")

6. Die Projektmappe sollte nun wie folgt aussehen:

![](images/6.png?raw=true "Abbildung 6")

###Aufgabe 4 - Seiten�bergreifende Navigationsleiste bearbeiten

1. �ffnen Sie die Datei **Views/Shared/_Layout.cshtml**
2. Finden Sie das Markup f�r die Auflistung der Men�navigation:

    ```XML
	<ul class="nav navbar-nav"> 
    ```
	
3. F�gen Sie dieser Auflistung einen weiteren **ActionLink** hinzu mit dem Titel **Posts** hinzu, der die Action **Index** innerhalb des *PostsController* aufruft

    ```XML
	<li>@Html.ActionLink("Posts", "Index", "Posts", new { area = "" }, null)</li>
	```
	
4. �ffnen Sie die Datei **Views/Home/Index.cshtml**
5. Finden Sie das Element, dass das Bild eines Posts anzeigt

    ```XML
	<a href="~/Uploads/@item.Image.FileName"><img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" style="vertical-align:middle" /></a>
    ```
	
6. F�gen Sie **unterhalb** dieses Elements einen **ActionLink** mit dem Titel **Details** ein, der die Action **Details** innerhalb des **PostsController** aufruft

    ```XML
	<span>@Html.ActionLink("Details", "Details", "Posts", new { id = item.Id }, null)</span>
    ```
	
7. Speichern Sie Ihre �nderungen und starten Sie die Anwendung
8. Die Anwendung sollte nun wie folgt aussehen:

![](images/7.png?raw=true "Abbildung 7")

9. Klicken Sie in der **Navigationsleiste** auf den Eintrag **Posts**

![](images/8.png?raw=true "Abbildung 8")

##�bung 2 - Ansichten anpassen
###Aufgabe 5 - Mehr Felder in der �bersicht aller Posts

1. �ffnen sie die Datei **Views/Posts/Index.cshtml**
2. Ersetzen Sie den Inhalt der Datei mit folgendem:

    ```XML
	@model IEnumerable<WebAdminAndApi.Models.Post>
	@{
		ViewBag.Title = "Posts";
	}
	<h2>Posts</h2>
	<p>
		@Html.ActionLink("Hinzuf�gen", "Create")
	</p>

	<table class="table">
		<tr>
			<th>
				#
			</th>
			<th>
				@Html.DisplayNameFor(model => model.Title)
			</th>
			<th>
				Bild
			</th>
			<th>
				User
			</th>
			<th>
				Likes
			</th>
			<th>
				Datum
			</th>
			<th></th>
		</tr>
		@foreach (var item in Model)
		{
			<tr>
				<td>
					@Html.DisplayFor(m => item.Id)
				</td>
				<td>
					@Html.DisplayFor(modelItem => item.Title)
				</td>
				<td>
					<img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" />
				</td>
				<td>
					@Html.DisplayFor(m => item.User.Name)
				</td>
				<td>
					@Html.DisplayFor(m => item.Likes.Count)
				</td>
				<td>
					@Html.DisplayFor(m => item.CreatedShort)
				</td>
				<td>
					@Html.ActionLink("Bearbeiten", "Edit", new { id = item.Id }) |
					@Html.ActionLink("Details", "Details", new { id = item.Id }) |
					@Html.ActionLink("L�schen", "Delete", new { id = item.Id })
				</td>
			</tr>
		}
	</table>
    ```
	
###Aufgabe 6 - Detailansicht eines Posts erweitern
	
3. �ffnen Sie die Datei **Views/Posts/Details.cshtml**
4. Suchen Sie **Description List** Element

    ```XML
	<dl class="dl-horizontal">
    ```

5. F�gen Sie der **Description List** ein Anzeigepaar hinzu, dass die **Anzahl** der **Likes** f�r den **aktuellen Post** darstellt

    ```XML
	<dt>
		@Html.DisplayNameFor(model => model.Likes):
	</dt>
	<dd>
		@Html.DisplayFor(model => model.Likes.Count)
	</dd>
    ```
	
6. F�gen Sie am Anfang der **Description List** ein Anzeigepaar hinzu, dass den **Namen des Erstellers** f�r den **aktuellen Post** ausgbibt

    ```XML
        <dt>
            @Html.DisplayNameFor(model => model.User):
        </dt>
        <dd>
            @Html.DisplayFor(model => model.User.Name)
        </dd>
    ```
		
7. F�gen Sie unterhalb der **Description List** eine **Tabelle** ein, die die **letzten 10 Likes** jeweils mit **Datum und Benutzername** f�r den aktuellen Post ausgibt

    ```XML
    <div>
        <header><h3>Die letzten 10 Likes</h3></header>
        <p>
            <table class="table">
                <thead>
                    <tr>
                        <td>Benutzer</td>
                        <td>Datum</td>
                    </tr>
                </thead>
                <tbody>
                    @foreach (var like in @Model.Likes.OrderByDescending(l => l.Created).Take(10))
                    {
                        <tr>
                            <td>@Html.DisplayFor(model => like.User.Name)</td>
                            <td>@Html.DisplayFor(model => like.Created)</td>
                        </tr>
                    }
                </tbody>
            </table>
        </p>
    </div>
    ```

8. F�gen Sie als erstes Element der **Description List** ein **Link-Element** ein, das das Bild des aktuellen Posts ausgibt

    ```XML
        <dd>
            <a href="~/Uploads/@Model.Image.FileName"><img src="~/Uploads/@Model.Image.FileName" width="250" alt="Bild" /></a>
        </dd>
    ```

9. Speichern Sie Ihre �nderungen und starten Sie die Anwendung
10. Rufen Sie die Detailansicht eines Posts auf
11. Ihre Anwendung sollte nun wie folgt aussehen:

![](images/9.png?raw=true "Abbildung 9")

###Aufgabe 7 - Seite um einen CSS-Style erweitern

1. �ffnen sie die Datei **Content/Site.css**
2. F�gen Sie folgenden CSS-Style ein

    ```CSS
	.btn-file {
		position: relative;
		overflow: hidden;
	}

    .btn-file input[type=file] {
        position: absolute;
        top: 0;
        right: 0;
        min-width: 100%;
        min-height: 100%;
        font-size: 100px;
        text-align: right;
        filter: alpha(opacity=0);
        opacity: 0;
        outline: none;
        background: white;
        cursor: inherit;
        display: block;
    }
    ```
	
###Aufgabe 8 - Bearbeitungsansicht f�r einen Post um ein Upload-Feld erweitern

1. �ffnen Sie die Datei **Views/Posts/Edit.cshtml**
2. Suchen Sie den Aufruf des **HtmlHelpers**, der den Bereich eines Formulares einleitet

    ```XML
	@using (Html.BeginForm())
    ```
	
3. Ersetzen Sie diesen Aufruf mit so, dass das Formular auch Bilder entgegen nehmen kann

    ```XML
	@using (Html.BeginForm(null, null, FormMethod.Post, new { enctype = "multipart/form-data" }))
    ```

4. Suchen Sie den Eintrag, der ein verstecktes Feld f�r die Eigenschaft **Id** des aktuellen Posts erzeugt

    ```XML
	@Html.HiddenFor(model => model.Id)
	```

5. F�gen Sie zwei versteckte Felder f�r die Eigenschaften **Image.Id** sowie **Image.FileName** hinzu
	
    ```XML
    @Html.HiddenFor(model => model.Image.Id)
    @Html.HiddenFor(model => model.Image.FileName)
	```

6. Suchen Sie die **form-group**, die den **Speichern-Button** umgibt

    ```XML
	<div class="form-group">
		<div class="col-md-offset-2 col-md-10">
			<input type="submit" value="Save" class="btn btn-default" />
		</div>
	</div>
    ```
	
7. F�gen Sie vor dieser **form-group** eine weitere **form-group** ein, die das aktuelle Bild des Posts anzeigt sowie ein **Upload-Feld** bereitstellt

    ```XML
	<div class="form-group">
		<span class="control-label col-md-2">Bild</span>
		<div class="col-md-10">
			<span class="btn btn-default btn-file">
				Durchsuchen
				<input type="file" name="file" id="file" />
			</span>
			@Html.ValidationMessage("ImageUpload")
		</div>
	</div>
    ```
	
8. Speichern Sie Ihre �nderungen und starten Sie die Anwendung
9. Ihre Anwendung sollte nun wie folgt aussehen:

	
![](images/10.png?raw=true "Abbildung 10")

###Aufgabe 9 - Ansicht zum Erstellen eines Posts um ein Upload-Feld erweitern

1. �ffnen Sie die Datei **Views/Posts/Create.cshtml**
2. Suchen Sie den Aufruf des **HtmlHelpers**, der den Bereich eines Formulares einleitet

    ```XML
	@using (Html.BeginForm())
    ```
	
3. Ersetzen Sie diesen Aufruf mit so, dass das Formular auch Bilder entgegen nehmen kann

    ```XML
	@using (Html.BeginForm(null, null, FormMethod.Post, new { enctype = "multipart/form-data" }))
    ```
	
4. Suchen Sie die **form-group**, die den **Speichern-Button** umgibt

    ```XML
	<div class="form-group">
		<div class="col-md-offset-2 col-md-10">
			<input type="submit" value="Save" class="btn btn-default" />
		</div>
	</div>
    ```

5. F�gen Sie oberhalb der letzten **form-group** eine neue **form-group** ein, in **Upload-Feld** angezeigt wird

    ```XML
	<div class="form-group">
		<span class="control-label col-md-2">Bild</span>
		<div class="col-md-10">
			<span class="btn btn-default btn-file">
				Durchsuchen
				<input type="file" name="file" id="file" />
			</span>
			@Html.ValidationMessage("ImageUpload")
		</div>
	</div>
    ```

6. **Suchen** und **l�schen** Sie die **form-group** f�r die Eigenschaften **Created**

    ```XML
	<div class="form-group">
		@Html.LabelFor(model => model.Created, htmlAttributes: new { @class = "control-label col-md-2" })
		<div class="col-md-10">
			@Html.EditorFor(model => model.Created, new { htmlAttributes = new { @class = "form-control" } })
			@Html.ValidationMessageFor(model => model.Created, "", new { @class = "text-danger" })
		</div>
	</div>
    ```

7. Speichern Sie Ihre �nderungen und starten Sie die Anwendung

##Zusammenfassung
In diesem Hands-On haben Sie gelernt
-Was Scaffolding ist und wie man es verwendet, um Ansicht auf Basis einer Vorlage anzulegen
-Wie man Entitysets gezielt nach Eintr�gen filtert
-Wie man den Razor-Syntax verwendet, um Eigenschaften des zugrundeliegenden Datenmodells anzuzeigen
-Wie man �ber das Absenden eines Formulars nicht nur Zeichenketten und Zahlen, sondern auch Bilder �bermitteln kann
-Wie man die seiten�bergreifende Navigationsleiste bearbeiten kann
-Wie man CSS-Style zu einer Seite hinzuf�gt und dieses anwendet