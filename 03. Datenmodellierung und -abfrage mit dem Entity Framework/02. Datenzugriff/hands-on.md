Modul 3/02 - Datenmodellierung und -abfrage mit dem Entity Framework: Datenzugriff
=======================================

##�bersicht 

##Ziele

##�bungen

###Aufgabe 1
1. Arbeiten Sie an Ihrer bereits vorhandenen Projektmappe weiter oder �ffnen Sie die fertige Projektmappe aus dem vorherigen Hands-On.
2. Erzeugen Sie einen neuen Ordner Utils im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf das Projekt im Projektmappen-Explorer tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
3. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **Utils** und w�hlen **Hinzuf�gen/Vorhandenes Element**.
4. Im Dialogfeld navigieren Sie in den Ordner **Files/Utils** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
5. Die Projektmappe sollte nun wie folgt aussehen:
![](images/1.png?raw=true "Abbildung 1")
6. �ffnen Sie die Datei **Utils/ImageUtility.cs** oder �ffnen Sie in der Aufgabenliste die erste Aufgabe 
7. Navigieren Sie zu der Funktion **ResizeImageAndSaveToDisk**
8. Verpassen Sie den hochgeladenen Bildern ein Wasserzeichen mit **Ihrem Namen**

    ```C#
	webImage = webImage.AddTextWatermark(".NET Jumpstart");
    ```
	
9. Speichern und schlie�en Sie die Datei

###Aufgabe 2
1. �ffnen Sie die Datei **Controllers/HomeController**
2. Stellen Sie sicher, dass folgende using-Direktiven im ViewModel enthalten sind:

    ```C#
	using WebAdminAndApi.Models;
    ```
	
3. �ndern Sie die Methode **Index()** so ab, dass Sie die 10 neuesten Posts anzeigt

    ```C#
	private ImageAppDbContext db = new ImageAppDbContext();

	public ActionResult Index()
	{
		var posts = db.Posts.OrderByDescending(p => p.Created).Take(10);
		return View(posts.ToList());
	}
	```
	
4. Speichern und schlie�en Sie die Datei

###Aufgabe 3
1. Machen Sie einen Rechtsklick auf den neu Ordner **Controllers** und w�hlen **Hinzuf�gen/Vorhandenes Element**.
2. Im Dialogfeld navigieren Sie in den Ordner **Files/Controllers** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
3. Die Projektmappe sollte nun wie folgt aussehen:
![](images/2.png?raw=true "Abbildung 2")
4. Starten Sie Ihre Anwendung �ber die Men�leiste **Debuggen/Debugging starten** oder das Tastenk�rzel **F5**
![](images/3.png?raw=true "Abbildung 3")
5. Beenden Sie das Debugging durch das Schlie�en des Browsers oder �ber die Men�leiste **Debugging/Debugging beenden** innerhalb von Visual Studio

##�bung 2

###Aufgabe 4
1. Klicken Sie in der Men�leiste auf **Ansicht/Server-Explorer**
2. Ihre Projektmappe sollte nun wie folgt aussehen:
![](images/4.png?raw=true "Abbildung 4")
3. Machen Sie einen Rechtsklick in den **Server-Explorer** und w�hlen Sie den Men�punkt **Verbindung hinzuf�gen**
4. Der Assistent, der sich �ffnet, sollte wie folgt aussehen:
![](images/5.png?raw=true "Abbildung 5")
5. �ffnen Sie die **Windows-Eingabeaufforderung** �ber **Start/Ausf�hren/cmd.exe**
6. F�hren Sie den Befehl **sqllocaldb.exe info v11.0** aus
![](images/6.png?raw=true "Abbildung 6")
7. Kopieren Sie den Wert des Attributes **Instance pipe name**, indem Sie den Text markieren und anschlie�end die rechte Maustaste dr�cken
8. Wechseln Sie zur�ck nach Visual Studio
9. Unter Servername setzen Sie den zuvor gespeicherten Wert von **Instance pipe name** ein
10. Unter �W�hlen Sie einen Datenbanknamen aus, oder geben Sie ihn ein� w�hlen Sie **aspnet-DotNETJumpStart-20150917082028**
11. Der Assistent sollte nun wie folgt aussehen:
![](images/7.png?raw=true "Abbildung 7")
12. Pr�fen Sie mit einem Klick auf **Testverbindung**, ob die Verbindungsvariablen korrekt eingegeben wurden
![](images/8.png?raw=true "Abbildung 8")
13. Verlassen Sie den Assistenten durch ein Dr�cken auf **OK**.

###Aufgabe 4
1. �ffnen Sie den **Server-Explorer**
2. Suchen Sie die zuvor hinzugef�gte Verbindung zur lokalen Datenbank und klappen Sie sie auf
3. Klappen Sie die 
4. Ihr Server-Explorer sollte nun wie folgt aussehen:
![](images/9.png?raw=true "Abbildung 9")
5. Machen Sie einen Rechtsklick auf die Tabelle **Post** und w�hlen den Men�eintrag **Tabellendaten anzeigen**
6. Ihre Projektmappe sollte nun wie folgt aussehen
![](images/10.png?raw=true "Abbildung 10")