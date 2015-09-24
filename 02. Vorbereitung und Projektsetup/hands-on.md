Modul 2 - Vorbereitung und Projektsetup
=======================================

##Übersicht
In dieser Hands-On-Session werden Sie ein Konto bei Microsoft anlegen und die Projektmappe in Visual Studio einrichten, die in den darauffolgenden Hands-On-Sessions verwendet wird.

##Ziele
- Ein Microsoft-Konto anlegen
- Die Projektmappe mit einer ASP.NET Webanwendung erstellen
- Die Entwicklungspakete über NuGet zu aktualisieren
- Die Webanwendung starten

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Anlegen eines Microsoft-Kontos</a><br/>
2. <a href="#Exercise2">Erstellen einer ASP.NET Webanwendung</a>

<a name="Exercise1"></a>
##Übung 1: Microsoft-Konto anlegen
Für das Modul 6 zum Thema App-Entwicklung wird eine Entwicklerlizenz von Microsoft benötigt. Diese ist zum Entwickeln und Testen von Apps kostenlos. Hierfür benötigen Sie ein Konto bei Microsoft. Falls Sie bereits ein Konto besitzen, können Sie diese Aufgabe überspringen.

###Aufgabe 1 - Microsoft-Konto anlegen
In diesem Schritt legen Sie Ihr persönliches Microsoft-Konto an.

1. Navigieren Sie zu <a href="http://www.microsoft.com/de-de/account/" target="_blank">http://www.microsoft.com/de-de/account/</a>.
2. Wählen Sie **Erstellen Sie ein kostenloses Microsoft Konto**.<br/><br/>
   ![](images/account-1.png?raw=true "Abbildung 0a")
3. Bei der Registrierung können Sie eine bereits bestehende E-Mail-Adresse als Konto verwenden oder eine neue E-Mail-Adresse bei Microsoft anlegen.<br/><br/>
   ![](images/account-2.png?raw=true "Abbildung 0b")

Sie haben nun ein Microsoft-Konto angelegt und damit die nötige Vorbereitung für Modul 6 getroffen.

<a name="Exercise2"></a>
##Übung 2: Erstellen einer ASP.NET Webanwendung

In dieser Übung werden Sie das Webprojekt in Visual Studio über eine der Projektvorlagen erstellen, die Entwicklungspakete auf den neusten Stand bringen und die Webanwendung starten.

###Aufgabe 1 - Erstellen einer ASP.NET Webanwendung mit MVC und Web API
In dieser Aufgabe wird das Projekt in Visual Studio angelegt.

1. Starten Sie **Visual Studio**.
2. In Visual Studio wählen Sie **Datei/Neu/Projekt**.
3. Im Dialog **Neues Projekt**:
   1. Wählen Sie **Vorlagen/Visual C#/Web**.
   2. Wählen Sie **ASP.NET-Webanwendung**
   3. Nennen Sie das Projekt **DotNETJumpStart** und bestätigen mit **OK**.<br/><br/>
   ![](images/new-web-project.png?raw=true "Abbildung 1")
4. Im Dialog für das **ASP.NET-Projekt**:
   1. Wählen Sie **Web API** aus.<br/><br/>
   ![](images/web-project-types.png?raw=true "Abbildung 2")
   2. Klicken Sie auf **Authentifizierung ändern**.
   3. Wählen Sie **Einzelne Benutzerkonten**.<br/><br/>
   ![](images/web-project-auth.png?raw=true "Abbildung 3")
   4. Deselektieren Sie den Haken **In der Cloud hosten**, denn das Projekt soll lokal gehostet werden.
   5. Wählen Sie **OK**.

Das Projekt wird nun in Visual Studio erstellt und Sie können mit der nächsten Aufgabe fortfahren.

###Aufgabe 2 - NuGet-Pakete aktualisieren
In dieser Aufgabe werden Sie die NuGet-Pakete für das eben erstellte Projekt aktualisieren. 

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **DotNETJumpStart** und wählen **NuGet-Pakete verwalten...**".<br/><br/>
   ![](images/manage-nuget-packages.png?raw=true "Abbildung 4")
2. Im Dialogfeld wählen Sie links **Aktualisierungen/nuget.org** und daraufhin **Alle aktualisieren**.<br/><br/>
   ![](images/update-nuget-packages.png?raw=true "Abbildung 5")

Nach einem Moment sind alle Pakete für die Entwicklung auf dem neusten Stand und Sie sind bereit mit der Entwicklung zu starten.

###Aufgabe 3 - Webanwendung im IIS-Express starten
In dieser Aufgabe werden Sie die Webanwendung starten.

1. Drücken Sie **F5**, um die Webanwendung mit Debugging zu starten.
2. Der IIS-Webserver wird automatisch gestartet und ein Tab im Webbrowser geöffnet.
3. Sie sollten nun die Startseite mit der ASP.NET-Standardvorlage sehen:<br/><br/>
    ![](images/asp.net-projekt.png?raw=true "Abbildung 6")
4. Beenden Sie das aktive Debugging mit Umschalt+F5.

###Aufgabe 4 - Machen Sie sich mit der Projektmappe vertraut
Für die nächsten Sessions werden Sie viel innerhalb der Projektstruktur arbeiten. Somit sollen Sie sich mit der Projektmappe vertraut machen.

1. Öffnen Sie die Datei **Index.cshtml** aus dem Ordner **Views/Home** über den **Projektmappen-Explorer**.
2. Ändern Sie ein paar Inhalte im HTML-Quellcode.
3. Starten Sie das Debugging und sehen sich die Änderungen im Browser an.
4. Öffnen Sie die Datei **_Layout.cshtml** im Ordner **Views/Shared**.
5. Machen Sie sich mit dem Aufbau und Zweck der Datei vertraut.
6. Beenden Sie das Debugging.
7. Öffnen Sie die Datei **HomeController.cs** im Ordner **Controllers**.
8. Ändern Sie den Wert von **ViewBag.Title** und Starten das Debugging.
9. Sehen Sie sich die Veränderung im Browser an und versuchen Sie herauszufinden, wo der Zugriff auf die Eigenschaft stattfindet.
10. Studieren Sie die restlichen Ordner des Projekts. Die wichtigsten Ordner sind:
	- Der Ordner **App_Start** enthält  Klassen zur Konfiguration des Projekts.
	- Der Ordner **Controllers** enthält die Controller zu den einzelnen Views.
	- Im Ordner **Models** werden die Models abgelegt, die von den Controllern und Views verwendet werden.
	- Der Ordner **Views** enthält alle HTML-Views und die Masterseite.

##Zusammenfassung
Mit Beendigung dieser Session haben Sie gelernt:  
- Eine ASP.NET Webanwendung aufzusetzen  
- NuGet-Pakete zu installieren  
- Die Webanwendung zu starten und zu beenden