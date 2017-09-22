# Modul 2 - Vorbereitung und Projektsetup

## Übersicht

In diesem Modul werden die Vorbereitungen für die darauffolgenden Hands-On-Sessions getroffen.

## Präsentation

Sehen Sie sich die [Präsentation](Vorbereitung%20und%20Projektsetup.pptx) zu diesem Modul an.

## Ziele

In diesem Hands-On werden Sie ein Konto bei Microsoft anlegen und die Projektmappe in Visual Studio einrichten, die in den darauffolgenden Hands-On-Sessions verwendet wird.

- Ein Microsoft-Konto anlegen
- Die Projektmappe mit einer ASP.NET Webanwendung erstellen
- Die Entwicklungspakete über NuGet aktualisieren
- Die Webanwendung starten

---

## Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Anlegen eines Microsoft-Kontos</a><br/>
2. <a href="#Exercise2">Erstellen einer ASP.NET Webanwendung</a>

<a name="Exercise1"></a>
### Übung 1: Microsoft-Konto anlegen
Um Visual Studio in der Entwicklungsumgebung verwenden zu können, wird ein kostenloses Microsoft-Konto benötigt. Falls Sie bereits ein Konto bei Microsoft besitzen, können Sie diese Übung überspringen.

#### Aufgabe 1 - Microsoft-Konto anlegen
In diesem Schritt legen Sie Ihr persönliches Microsoft-Konto an.

1. Navigieren Sie zu <a href="http://www.microsoft.com/de-de/account/" target="_blank">http://www.microsoft.com/de-de/account/</a>.
2. Wählen Sie **Erstellen Sie ein kostenloses Microsoft Konto**.<br/><br/>
   ![](_images/account-1.png?raw=true "Abbildung 0a")
3. Bei der Registrierung können Sie eine bereits bestehende E-Mail-Adresse als Konto verwenden oder eine neue E-Mail-Adresse bei Microsoft anlegen.<br/><br/>
   ![](_images/account-2.png?raw=true "Abbildung 0b")

Starten Sie nun Visual Studio. Auf der Entwicklungsumgebung ist die kostenfreie **Community-Edition** installiert. Sie müssen sich nach dem Start mit Ihrem Microsoft-Konto anmelden, um Visual Studio verwenden zu können.

<a name="Exercise2"></a>
### Übung 2: Erstellen einer ASP.NET Webanwendung

In dieser Übung werden Sie das Webprojekt in Visual Studio erstellen, die Entwicklungspakete auf den neusten Stand bringen und die Webanwendung starten.

#### Aufgabe 1 - Erstellen einer ASP.NET Webanwendung mit MVC und Web API
In dieser Aufgabe wird das Projekt in Visual Studio angelegt.

1. Starten Sie **Visual Studio**.
2. In Visual Studio wählen Sie **Datei/Neu/Projekt**.
3. Im Dialog **Neues Projekt**:
   1. Wählen Sie **Vorlagen/Visual C#/Web**.
   2. Wählen Sie **ASP.NET-Webanwendung (.NET Framework)**.
   3. Benennen Sie das Projekt **DotNETJumpStart** und bestätigen mit **OK**.<br/><br/>
   ![](_images/new-web-project.png?raw=true "Abbildung 1")
4. Im Dialog für das **ASP.NET-Projekt**:
   1. Wählen Sie **Web API** aus.<br/><br/>
   ![](_images/web-project-types.png?raw=true "Abbildung 2")
   2. Klicken Sie auf **Authentifizierung ändern**.
   3. Wählen Sie **Keine Authentifizierung**.<br/><br/>
   ![](_images/web-project-auth.png?raw=true "Abbildung 3")
   4. Deselektieren Sie den Haken **In der Cloud hosten**, denn das Projekt soll lokal gehostet werden.
   5. Wählen Sie **OK**.

Das Projekt wird nun von Visual Studio erstellt. Dies kann unter Umständen etwas dauern, da diverse Pakete heruntergeladen werden.

#### Aufgabe 2 - NuGet-Pakete aktualisieren und installieren
In dieser Aufgabe werden Sie die NuGet-Pakete für das eben erstellte Projekt aktualisieren. 

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **DotNETJumpStart** und wählen **NuGet-Pakete verwalten...**".<br/><br/>
   ![](_images/manage-nuget-packages.png?raw=true "Abbildung 4")
2. Im Paketmanager, unter dem Reiter "**Aktualisierungen**", wählen Sie links oben **Alle Pakete auswählen** und daraufhin **Aktualisieren**.<br/><br/>
   ![](_images/update-nuget-packages.png?raw=true "Abbildung 5")
   
3. **Entity Framework** installieren:   
   Auf den Reiter Durchsuchen klicken und Entity Framework eingeben. Das Entity Framework installieren.
   ![](_images/install-entity-framework.png?raw=true "Abbildung 6")

Nach einem Moment sind alle Pakete auf dem neusten Stand und Sie sind bereit mit der Entwicklung zu starten.  
Hinweis: Eventuell ist ein Neustart von Visual Studio notwendig.



#### Aufgabe 3 - Webanwendung im IIS-Express starten
In dieser Aufgabe werden Sie die Webanwendung starten.

1. Starten Sie mit **F5** die Webanwendung mit Debugging.
2. Der IIS-Webserver wird automatisch gestartet und ein Tab im Webbrowser geöffnet.
3. Sie sollten nun die Startseite mit der ASP.NET-Standardvorlage sehen:<br/><br/>
    ![](_images/asp.net-projekt.png?raw=true "Abbildung 7")
4. Schließen Sie das Browserfenster und beenden Sie das aktive Debugging mit Umschalt+F5.

#### Aufgabe 4 - Machen Sie sich mit der Projektmappe vertraut
Für die nächsten Sessions werden Sie viel innerhalb der Projektstruktur arbeiten. Somit sollen Sie sich mit der Projektmappe vertraut machen.

1. Öffnen Sie die Datei **Index.cshtml** aus dem Ordner **Views/Home** über den **Projektmappen-Explorer**.
2. Ändern Sie ein paar Inhalte im HTML-Quellcode.
3. Starten Sie das Debugging und sehen sich die Änderungen im Browser an.
4. Beenden Sie das Debugging.
5. Öffnen Sie die Datei **_Layout.cshtml** im Ordner **Views/Shared**.
6. Machen Sie sich mit dem Aufbau und Zweck der Datei vertraut.
7. Öffnen Sie die Datei **HomeController.cs** im Ordner **Controllers**.
8. Ändern Sie den Wert von **ViewBag.Title** und Starten das Debugging.
9. Sehen Sie sich die Veränderung im Browser an und versuchen Sie herauszufinden, wo und wie der Zugriff auf den **ViewBad.Title**, innerhalb der HTML-View stattfindet.
10. Studieren Sie die restlichen Ordner des Projekts. Die wichtigsten Ordner sind:
	- Der Ordner **App_Start** enthält  Klassen zur Konfiguration des Projekts.
	- Der Ordner **Controllers** enthält die Controller zu den einzelnen Views.
	- Im Ordner **Models** werden die Models abgelegt, die von den Controllern und Views verwendet werden.
	- Der Ordner **Views** enthält alle HTML-Views und die Masterseite.

## Zusammenfassung

Mit Beendigung dieser Session haben Sie gelernt:  
- Eine ASP.NET Webanwendung aufzusetzen  
- NuGet-Pakete zu installieren  
- Die Webanwendung zu starten
