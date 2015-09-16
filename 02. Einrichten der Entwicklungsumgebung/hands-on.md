Modul 2 - Einrichten der Projektmappe
=======================================

##Übersicht
In diesem Hands-On werden Sie ein Konto bei Microsoft anlegen und die Projektmappe in Visual Studio einrichten, die in den darauffolgenden Hands-On-Sessions verwendet wird.

##Ziele
- Ein Microsoft-Konto anlegen
- Die Projektmappe mit einer ASP.NET Webanwendung erstellen
- Die Entwicklungspakete aktualisieren
- Die Webanwendung starten

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Anlegen eines Microsoft-Kontos</a><br/>
2. <a href="#Exercise2">Erstellen einer ASP.NET Webanwendung</a>

<a name="Exercise1"></a>
##Übung 1: Microsoft-Konto anlegen
Für das Modul zur App-Entwicklung wird eine Entwicklerlizenz von Microsoft benötigt. Diese ist zum Entwickeln und Testen kostenlos. Hierfür benötigen Sie ein Konto bei Microsoft. Falls Sie bereits ein Konto besitzen, können Sie diese Aufgabe überspringen.

###Aufgabe 1 - Microsoft-Konto anlegen
In diesem Schritt legen Sie Ihr persönliches Microsoft-Konto an.

1. Navigieren Sie zu <a href="http://www.microsoft.com/de-de/account/">http://www.microsoft.com/de-de/account/</a>.
2. Wählen Sie **Erstellen Sie ein kostenloses Microsoft Konto**.<br/>
   ![](images/account-1.png?raw=true "Abbildung 0a")
3. Bei der Registrierung können Sie eine bereits bestehende E-Mail-Adresse als Ihr Konto verwenden oder eine neue Adresse bei Microsoft anlegen.<br/>
   ![](images/account-2.png?raw=true "Abbildung 0b")

Sie haben nun ein Microsoft-Konto angelegt.

<a name="Exercise2"></a>
##Übung 2: Erstellen einer ASP.NET Webanwendung

In dieser Übung werden Sie das Webprojekt in Visual Studio mit der benötigten Vorlage erstellen und die Entwicklungspakete auf den neusten Stand bringen.

###Aufgabe 1 - Erstellen einer ASP.NET Webanwendung mit MVC und Web API
In diesem Schritt wird das Projekt in Visual Studio angelegt.

1. Starten Sie **Visual Studio**.
2. In Visual Studio wählen Sie **Datei/Neu/Projekt**.
3. Im Dialog **Neues Projekt**:
   1. Wählen Sie **Vorlagen/Visual C#/Web**.
   2. Wählen Sie **ASP.NET-Webanwendung**
   3. Nennen Sie das Projekt **DotNETJumpStart** und bestätigen mit **OK**.<br/>
   ![](images/new-web-project.png?raw=true "Abbildung 1")
4. Im Dialog für das **ASP.NET-Projekt**:
   1. Wählen Sie Web API<br/>
   ![](images/web-project-types.png?raw=true "Abbildung 2")
   2. Klicken Sie auf **Authentifizierung ändern**
   3. Wählen Sie **Einzelne Benutzerkonten**<br/>
   ![](images/web-project-auth.png?raw=true "Abbildung 3")
   4. Falls der Haken **In der Cloud hosten** aktiviert sein sollte, so **wählen Sie ihn ab**. Das Projekt soll nur lokal gehostet werden.

Sie haben nun das benötigte Projekt erstellt.

###Aufgabe 2 - NuGet-Pakete aktualisieren
In dieser Aufgabe werden Sie die NuGet-Pakete für das eben erstellte Projekt aktualisieren. 

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **DotNETJumpStart** und wählen **NuGet-Pakete verwalten...**".<br/>
   ![](images/manage-nuget-packages.png?raw=true "Abbildung 4")
2. Im Dialogfeld wählen Sie links **Aktualisierungen** und daraufhin **Alle aktualisieren**<br/>
   ![](images/update-nuget-packages.png?raw=true "Abbildung 5")

Alle Pakete für die Entwicklung sind nun auf dem neusten Stand.

###Aufgabe 3 - Webanwendung im IIS-Express starten
In dieser Aufgabe werden Sie die Webanwendung starten.

1. Drücken Sie F5, um das Debugging zu starten
2. Der IIS-Webserver wird automatisch gestartet und ein Webbrowser geöffnet
3. Sie sollten nun die Startseite mit der ASP.NET-Standardvorlage sehen:<br/>
    ![](images/asp.net-projekt.png?raw=true "Abbildung 6")
4. Beenden Sie das aktive Debugging mit Umschalt+F5.

Sie haben nun ein lauffähiges Webprojekt für die nächsten Sessions.

###Aufgabe 4 - Die Webanwendung ohne Debugging starten
Für die eigentliche Entwicklung der Webanwendung wird nicht immer ein aktiver Debugger benötigt. Die Entwicklung wird dadurch in vielen Fällen effizienter.

1. Sie können die Anwendung ohne Debugging mit **Strg+F5** starten.

Hierbei sind folgende Dinge zu beachten:

- Damit Änderungen im C#-Quellcode im Browser sichtbar werden, müssen Sie die Anwendung neu kompilieren. Hierzu drücken Sie entweder wieder **Strg+F5** oder wählen in Visual Studio **Erstellen/Projektmappe erstellen**.
- Änderungen im HTML müssen nicht neu kompiliert werden. Hierzu reicht es, die HTML-Datei zu speichern und das Browserfenster zu aktualisieren.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Eine ASP.NET Webanwendung aufzusetzen  
- NuGet-Pakete zu installieren  
- Die Webanwendung ohne Debugging zu starten  