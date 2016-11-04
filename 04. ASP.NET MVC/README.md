# Modul 4 - Aufbau einer Webapplikation mit ASP.NET MVC: Erstellen von Businesslogik und Views zum Verwalten der Posts-Entität

## Übersicht 

In diesem Modul lernen Sie die Grundlagen zu ASP.NET MVC kennen.

TODO: Ziele dieses Modus
TODO: Ankersprünge zu Übungen

## Präsentation

Sehen Sie sich die zu dem Modul gehörende [Präsentation](04. Aufbau einer Webapplikation mit ASP.NET MVC.pptx) an.

## Ziele

In diesem Hands-On lernen Sie   
-Wie man Scaffolding verwendet, um Ansicht auf Basis einer Vorlage anzulegen  
-Wie man Entitysets gezielt nach Einträgen filtert  
-Wie man den Razor-Syntax verwendet, um Eigenschaften des zugrundeliegenden Datenmodells anzuzeigen  
-Wie man über das Absenden eines Formulars nicht nur Zeichenketten und Zahlen, sondern auch Bilder übermitteln kann  
-Wie man die seitenübergreifende Navigationsleiste bearbeiten kann  
-Wie man CSS-Style zu einer Seite hinzufügt und dieses anwendet  

---

## Übungen

1. Businesslogik und Scaffolding
2. Erweiterung der Views

### Übung 1 - Bereitstellen der Businesslogik
In dieser Übung werden wir: ...

#### Aufgabe 1 - Erweitern des PostsController

1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Machen Sie einen Rechtsklick auf den Ordner **Controllers** und wählen **Hinzufügen/Vorhandenes Element**
3. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Controllers** aus dem aktuellen Hands-On und wählen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer.png?raw=true "Abbildung 1")

5. Öffnen Sie die Datei **PostsController.cs**
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
                image = db._images.Add(image);
                db.SaveChanges();

                // Assign the image to the post
                post.Image = image;

                // Only the admin can post _images here, so select admin
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

#### Aufgabe 2 - Vorbereiten der Projektmappe für neue Ansichten

1. Erzeugen Sie einen neuen Ordner **Views/Posts** im aktuellen Projekt. Sie können das über einen Rechtsklick auf den Ordner **Views** im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Die Projektmappe sollte nun wie folgt aussehen:
	
![](_images/solution-explorer-2.png?raw=true "Abbildung 2")

#### Aufgabe 3 - Hinzufügen von Ansichten für das Bearbeiten, Editieren und Löschen von Posts

1. Wählen Sie den Ordner **Views/Posts** an und fügen ihm über einen Rechtsklick durch einen Klick auf die Schaltfläche **Hinzufügen/Neues Gerüstelement** eine neue Ansicht hinzu
2. Wählen Sie im aufgehenden Dialog die Vorlage **MVC5-Ansicht**

![](_images/mvc-view.png?raw=true "Abbildung 3")

3. Ersellen Sie eine Ansicht unter der Vorlage **Create** mit dem Namen **Create** für die Modellklasse **Post** innerhalb der Datenkontextklasse **ImageAppDbContext**

![](_images/scaffolding-1.png?raw=true "Abbildung 4")

4. Wiederholen Sie die Schritte **1** bis **3** für die Ansichten mit den Namen **Delete**, **Details** und **Edit** sowie deren korrespondierenden Vorlagen
5. Erstellen Sie eine Ansicht unter der Vorlage **List** mit dem Namen **Index** für die Modellklasse **Post** innerhalb der Datenkontextklasse **ImageAppDbContext**

![](_images/scaffolding-2.png?raw=true "Abbildung 5")

6. Die Projektmappe sollte nun wie folgt aussehen:

![](_images/solution-explorer-3.png?raw=true "Abbildung 6")

#### Aufgabe 4 - Seitenübergreifende Navigationsleiste bearbeiten

1. Öffnen Sie die Datei **Views/Shared/_Layout.cshtml**
2. Finden Sie das Markup für die Auflistung der Menünavigation:

    ```XML
	<ul class="nav navbar-nav"> 
    ```
	
3. Fügen Sie dieser Auflistung einen weiteren **ActionLink** hinzu mit dem Titel **Posts** hinzu, der die Action **Index** innerhalb des *PostsController* aufruft

    ```XML
	<li>@Html.ActionLink("Posts", "Index", "Posts", new { area = "" }, null)</li>
	```
	
4. Öffnen Sie die Datei **Views/Home/Index.cshtml**
5. Finden Sie das Element, dass das Bild eines Posts anzeigt

    ```XML
	<a href="~/Uploads/@item.Image.FileName"><img src="~/Uploads/@item.Image.FileName" width="200" alt="Bild" style="vertical-align:middle" /></a>
    ```
	
6. Fügen Sie **unterhalb** dieses Elements einen **ActionLink** mit dem Titel **Details** ein, der die Action **Details** innerhalb des **PostsController** aufruft

    ```XML
	<span>@Html.ActionLink("Details", "Details", "Posts", new { id = item.Id }, null)</span>
    ```
	
7. Speichern Sie Ihre Änderungen und starten Sie die Anwendung
8. Die Anwendung sollte nun wie folgt aussehen:

![](_images/posts.png?raw=true "Abbildung 7")

9. Klicken Sie in der **Navigationsleiste** auf den Eintrag **Posts**

![](_images/posts-list.png?raw=true "Abbildung 8")

### Übung 2 - Ansichten anpassen
In dieser Übung...

#### Aufgabe 1 - Mehr Felder in der Übersicht aller Posts

1. Öffnen sie die Datei **Views/Posts/Index.cshtml**
2. Ersetzen Sie den Inhalt der Datei mit folgendem:

    ```XML
	@model IEnumerable<WebAdminAndApi.Models.Post>
	@{
		ViewBag.Title = "Posts";
	}
	<h2>Posts</h2>
	<p>
		@Html.ActionLink("Hinzufügen", "Create")
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
					@Html.ActionLink("Löschen", "Delete", new { id = item.Id })
				</td>
			</tr>
		}
	</table>
    ```
	

#### Aufgabe 2 - Detailansicht eines Posts erweitern
	
3. Öffnen Sie die Datei **Views/Posts/Details.cshtml**
4. Suchen Sie **Description List** Element

    ```XML
	<dl class="dl-horizontal">
    ```

5. Fügen Sie der **Description List** ein Anzeigepaar hinzu, dass die **Anzahl** der **Likes** für den **aktuellen Post** darstellt

    ```XML
	<dt>
		@Html.DisplayNameFor(model => model.Likes):
	</dt>
	<dd>
		@Html.DisplayFor(model => model.Likes.Count)
	</dd>
    ```
	
6. Fügen Sie am Anfang der **Description List** ein Anzeigepaar hinzu, dass den **Namen des Erstellers** für den **aktuellen Post** ausgbibt

    ```XML
        <dt>
            @Html.DisplayNameFor(model => model.User):
        </dt>
        <dd>
            @Html.DisplayFor(model => model.User.Name)
        </dd>
    ```
		
7. Fügen Sie unterhalb der **Description List** eine **Tabelle** ein, die die **letzten 10 Likes** jeweils mit **Datum und Benutzername** für den aktuellen Post ausgibt

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

8. Fügen Sie als erstes Element der **Description List** ein **Link-Element** ein, das das Bild des aktuellen Posts ausgibt

    ```XML
        <dd>
            <a href="~/Uploads/@Model.Image.FileName"><img src="~/Uploads/@Model.Image.FileName" width="250" alt="Bild" /></a>
        </dd>
    ```

9. Speichern Sie Ihre Änderungen und starten Sie die Anwendung
10. Rufen Sie die Detailansicht eines Posts auf
11. Ihre Anwendung sollte nun wie folgt aussehen:

![](_images/posts-details.png?raw=true "Abbildung 9")

#### Aufgabe 3 - Seite um einen CSS-Style erweitern

1. Öffnen sie die Datei **Content/Site.css**
2. Fügen Sie folgenden CSS-Style ein

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
	

#### Aufgabe 4 - Bearbeitungsansicht für einen Post um ein Upload-Feld erweitern

1. Öffnen Sie die Datei **Views/Posts/Edit.cshtml**
2. Suchen Sie den Aufruf des **HtmlHelpers**, der den Bereich eines Formulares einleitet

    ```XML
	@using (Html.BeginForm())
    ```
	
3. Ersetzen Sie diesen Aufruf mit so, dass das Formular auch Bilder entgegen nehmen kann

    ```XML
	@using (Html.BeginForm(null, null, FormMethod.Post, new { enctype = "multipart/form-data" }))
    ```

4. Suchen Sie den Eintrag, der ein verstecktes Feld für die Eigenschaft **Id** des aktuellen Posts erzeugt

    ```XML
	@Html.HiddenFor(model => model.Id)
	```

5. Fügen Sie zwei versteckte Felder für die Eigenschaften **Image.Id** sowie **Image.FileName** hinzu
	
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
	
7. Fügen Sie vor dieser **form-group** eine weitere **form-group** ein, die das aktuelle Bild des Posts anzeigt sowie ein **Upload-Feld** bereitstellt

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
	
8. Speichern Sie Ihre Änderungen und starten Sie die Anwendung
9. Ihre Anwendung sollte nun wie folgt aussehen:

	
![](_images/posts-edit.png?raw=true "Abbildung 10")

#### Aufgabe 5 - Ansicht zum Erstellen eines Posts um ein Upload-Feld erweitern

1. Öffnen Sie die Datei **Views/Posts/Create.cshtml**
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

5. Fügen Sie oberhalb der letzten **form-group** eine neue **form-group** ein, in **Upload-Feld** angezeigt wird

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

6. **Suchen** und **löschen** Sie die **form-group** für die Eigenschaften **Created**

    ```XML
	<div class="form-group">
		@Html.LabelFor(model => model.Created, htmlAttributes: new { @class = "control-label col-md-2" })
		<div class="col-md-10">
			@Html.EditorFor(model => model.Created, new { htmlAttributes = new { @class = "form-control" } })
			@Html.ValidationMessageFor(model => model.Created, "", new { @class = "text-danger" })
		</div>
	</div>
    ```

7. Speichern Sie Ihre Änderungen und starten Sie die Anwendung

## Zusammenfassung

In diesem Hands-On haben Sie gelernt:
- Was Scaffolding ist und wie man es verwendet, um Ansicht auf Basis einer Vorlage anzulegen  
- Wie man Entitysets gezielt nach Einträgen filtert  
- Wie man den Razor-Syntax verwendet, um Eigenschaften des zugrundeliegenden Datenmodells anzuzeigen  
- Wie man über das Absenden eines Formulars nicht nur Zeichenketten und Zahlen, sondern auch Bilder übermitteln kann  
- Wie man die seitenübergreifende Navigationsleiste bearbeiten kann  
- Wie man CSS-Style zu einer Seite hinzufügt und dieses anwendet  
