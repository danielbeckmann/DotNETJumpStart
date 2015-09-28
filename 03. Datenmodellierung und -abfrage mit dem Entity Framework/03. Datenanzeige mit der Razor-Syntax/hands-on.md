Modul 3/02 - Datenanzeige mit der Razor-Syntax
=======================================

##Ziele
- Razor-Syntax verwenden, um auf Eigenschaften eines ViewModels zuzugreifen
- Html-Syntax verwenden, um Daten aus einem ViewModel anzuzeigen

##Übungen

###Aufgabe 1 - Posts auf der Startseite anzeigen
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

![](images/1.png?raw=true "Abbildung 1")

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

![](images/2.png?raw=true "Abbildung 2")

15. Beenden Sie das Debugging

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie Sie Razor-Syntax verwenden, um auf Eigenschaften eines ViewModels zuzugreifen
- Wie Sie Html-Syntax verwenden, um Daten aus einem ViewModel anzuzeigen
- Dass Sie Änderungen am Code auch vornehmen können, während sich die Anwendung im Debugging befindet