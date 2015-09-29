Modul 3/02 - Datenmodellierung und -abfrage mit dem Entity Framework: Datenzugriff
=======================================

##Ziele
- Ein Bild aus einem HttpRequest speichern
- Einem Bild ein Wasserzeichen geben
- Daten festlegen, die ein View anzeigen sollte
- Mit einer LocalDB verbinden

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Hinzufügen der ImageUtils und Bearbeiten der Controller</a><br/>
2. <a href="#Exercise2">Mit LocalDB verbinden</a><br/>

<a name="Exercise1"></a>
##Übung 1 - Hinzufügen der ImageUtils und Bearbeiten der Controller

###Aufgabe 1 - Utilites zum Speichern von Bildern hinzufügen
1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder öffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Erzeugen Sie einen neuen Ordner **Utils** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
3. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Utils** und wählen **Hinzufügen/Vorhandenes Element**.
4. Im Dialogfeld navigieren Sie in den Ordner **Files/Utils** aus dem aktuellen Hands-On und wählen alle Dateien aus.
5. Die Projektmappe sollte nun wie folgt aussehen:

![](images/1.png?raw=true "Abbildung 1")

6. Öffnen Sie die Datei **Utils/ImageUtility.cs** oder öffnen Sie in der Aufgabenliste die erste Aufgabe 
7. Navigieren Sie zu der Funktion **ResizeImageAndSaveToDisk**
8. Verpassen Sie den hochgeladenen Bildern ein Wasserzeichen mit **Ihrem Namen**

    ```C#
	webImage = webImage.AddTextWatermark(".NET Jumpstart");
    ```
	
9. Speichern und schließen Sie die Datei

###Aufgabe 2 - HomeController bearbeiten
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

###Aufgabe 3 - PostsController hinzufügen
1. Machen Sie einen Rechtsklick auf den neu Ordner **Controllers** und wählen **Hinzufügen/Vorhandenes Element**.
2. Im Dialogfeld navigieren Sie in den Ordner **Files/Controllers** aus dem aktuellen Hands-On und wählen alle Dateien aus.
3. Die Projektmappe sollte nun wie folgt aussehen:

![](images/2.png?raw=true "Abbildung 2")

4. Starten Sie Ihre Anwendung über die Menüleiste **Debuggen/Debugging starten** oder das Tastenkürzel **F5**

![](images/3.png?raw=true "Abbildung 3")

5. Beenden Sie das Debugging durch das Schließen des Browsers oder über die Menüleiste **Debugging/Debugging beenden** innerhalb von Visual Studio

<a name="Exercise2"></a>
##Übung 2 - Mit LocalDB verbinden

###Aufgabe 4 - Visual Studio Server-Explorer verwenden, um eine Verbindung hinzuzufügen
1. Klicken Sie in der Menüleiste auf **Ansicht/Server-Explorer**
2. Ihre Projektmappe sollte nun wie folgt aussehen:

![](images/4.png?raw=true "Abbildung 4")

3. Machen Sie einen Rechtsklick in den **Server-Explorer** und wählen Sie den Menüpunkt **Verbindung hinzufügen**
4. Der Assistent, der sich öffnet, sollte wie folgt aussehen:

![](images/5.png?raw=true "Abbildung 5")

5. Öffnen Sie die **Windows-Eingabeaufforderung** über **Start/Ausführen/cmd.exe**
6. Führen Sie den Befehl **sqllocaldb.exe info v11.0** aus

![](images/6.png?raw=true "Abbildung 6")

7. Kopieren Sie den Wert des Attributes **Instance pipe name**, indem Sie den Text markieren und anschließend die rechte Maustaste drücken
8. Wechseln Sie zurück nach Visual Studio
9. Unter Servername setzen Sie den zuvor gespeicherten Wert von **Instance pipe name** ein
10. Unter „Wählen Sie einen Datenbanknamen aus, oder geben Sie ihn ein“ wählen Sie **aspnet-DotNETJumpStart-20150917082028**
11. Der Assistent sollte nun wie folgt aussehen:

![](images/7.png?raw=true "Abbildung 7")

12. Prüfen Sie mit einem Klick auf **Testverbindung**, ob die Verbindungsvariablen korrekt eingegeben wurden

![](images/8.png?raw=true "Abbildung 8")

13. Verlassen Sie den Assistenten durch ein Drücken auf **OK**.

###Aufgabe 5 - Tabelldaten anzeigen
1. Öffnen Sie den **Server-Explorer**
2. Suchen Sie die zuvor hinzugefügte Verbindung zur lokalen Datenbank und klappen Sie sie auf
3. Klappen Sie die 
4. Ihr Server-Explorer sollte nun wie folgt aussehen:

![](images/9.png?raw=true "Abbildung 9")

5. Machen Sie einen Rechtsklick auf die Tabelle **Post** und wählen den Menüeintrag **Tabellendaten anzeigen**
6. Ihre Projektmappe sollte nun wie folgt aussehen

![](images/10.png?raw=true "Abbildung 10")

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Wie eine Datei aus einem HttpRequest gespeichert werden kann
- Wie man einem Bild ein Overlay hinzugefügt
- Wie man LINQ verwendet
- Wie man sich mit einer LocalDB verbindet
