Modul 2 - Einrichten der Projektmappe
=======================================

##�bersicht
In diesem Hands-On werden Sie ein Konto bei Microsoft anlegen und die Projektmappe in Visual Studio einrichten, die in den darauffolgenden Hands-On-Sessions verwendet wird.

##Ziele
- Ein Microsoft-Konto anlegen
- Die Projektmappe mit einer ASP.NET Webanwendung erstellen
- Die Entwicklungspakete aktualisieren
- Die Webanwendung starten

##�bungen
Dieses Hands-On besteht aus den folgenden �bungen:<br/>
1. <a href="#Exercise1">Anlegen eines Microsoft-Kontos</a><br/>
2. <a href="#Exercise2">Erstellen einer ASP.NET Webanwendung</a>

<a name="Exercise1"></a>
##�bung 1: Microsoft-Konto anlegen
F�r das Modul zur App-Entwicklung wird eine Entwicklerlizenz von Microsoft ben�tigt. Diese ist zum Entwickeln und Testen kostenlos. Hierf�r ben�tigen Sie ein Konto bei Microsoft. Falls Sie bereits ein Konto besitzen, k�nnen Sie diese Aufgabe �berspringen.

###Aufgabe 1 - Microsoft-Konto anlegen
In diesem Schritt legen Sie Ihr pers�nliches Microsoft-Konto an.

1. Navigieren Sie zu <a href="http://www.microsoft.com/de-de/account/">http://www.microsoft.com/de-de/account/</a>.
2. W�hlen Sie **Erstellen Sie ein kostenloses Microsoft Konto**.<br/>
   ![](images/account-1.png?raw=true "Abbildung 0a")
3. Bei der Registrierung k�nnen Sie eine bereits bestehende E-Mail-Adresse als Ihr Konto verwenden oder eine neue Adresse bei Microsoft anlegen.<br/>
   ![](images/account-2.png?raw=true "Abbildung 0b")

Sie haben nun ein Microsoft-Konto angelegt.

<a name="Exercise2"></a>
##�bung 2: Erstellen einer ASP.NET Webanwendung

In dieser �bung werden Sie das Webprojekt in Visual Studio mit der ben�tigten Vorlage erstellen und die Entwicklungspakete auf den neusten Stand bringen.

###Aufgabe 1 - Erstellen einer ASP.NET Webanwendung mit MVC und Web API
In diesem Schritt wird das Projekt in Visual Studio angelegt.

1. Starten Sie **Visual Studio**.
2. In Visual Studio w�hlen Sie **Datei/Neu/Projekt**.
3. Im Dialog **Neues Projekt**:
   1. W�hlen Sie **Vorlagen/Visual C#/Web**.
   2. W�hlen Sie **ASP.NET-Webanwendung**
   3. Nennen Sie das Projekt **DotNETJumpStart** und best�tigen mit **OK**.<br/>
   ![](images/new-web-project.png?raw=true "Abbildung 1")
4. Im Dialog f�r das **ASP.NET-Projekt**:
   1. W�hlen Sie Web API<br/>
   ![](images/web-project-types.png?raw=true "Abbildung 2")
   2. Klicken Sie auf **Authentifizierung �ndern**
   3. W�hlen Sie **Einzelne Benutzerkonten**<br/>
   ![](images/web-project-auth.png?raw=true "Abbildung 3")
   4. Falls der Haken **In der Cloud hosten** aktiviert sein sollte, so **w�hlen Sie ihn ab**. Das Projekt soll nur lokal gehostet werden.

Sie haben nun das ben�tigte Projekt erstellt.

###Aufgabe 2 - NuGet-Pakete aktualisieren
In dieser Aufgabe werden Sie die NuGet-Pakete f�r das eben erstellte Projekt aktualisieren. 

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf das Projekt **DotNETJumpStart** und w�hlen **NuGet-Pakete verwalten...**".<br/>
   ![](images/manage-nuget-packages.png?raw=true "Abbildung 4")
2. Im Dialogfeld w�hlen Sie links **Aktualisierungen** und daraufhin **Alle aktualisieren**<br/>
   ![](images/update-nuget-packages.png?raw=true "Abbildung 5")

Alle Pakete f�r die Entwicklung sind nun auf dem neusten Stand.

###Aufgabe 3 - Webanwendung im IIS-Express starten
In dieser Aufgabe werden Sie die Webanwendung starten.

1. Dr�cken Sie F5, um das Debugging zu starten
2. Der IIS-Webserver wird automatisch gestartet und ein Webbrowser ge�ffnet
3. Sie sollten nun die Startseite mit der ASP.NET-Standardvorlage sehen:<br/>
    ![](images/asp.net-projekt.png?raw=true "Abbildung 6")
4. Beenden Sie das aktive Debugging mit Umschalt+F5.

Sie haben nun ein lauff�higes Webprojekt f�r die n�chsten Sessions.

###Aufgabe 4 - Die Webanwendung ohne Debugging starten
F�r die eigentliche Entwicklung der Webanwendung wird nicht immer ein aktiver Debugger ben�tigt. Die Entwicklung wird dadurch in vielen F�llen effizienter.

1. Sie k�nnen die Anwendung ohne Debugging mit **Strg+F5** starten.

Hierbei sind folgende Dinge zu beachten:

- Damit �nderungen im C#-Quellcode im Browser sichtbar werden, m�ssen Sie die Anwendung neu kompilieren. Hierzu dr�cken Sie entweder wieder **Strg+F5** oder w�hlen in Visual Studio **Erstellen/Projektmappe erstellen**.
- �nderungen im HTML m�ssen nicht neu kompiliert werden. Hierzu reicht es, die HTML-Datei zu speichern und das Browserfenster zu aktualisieren.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Eine ASP.NET Webanwendung aufzusetzen  
- NuGet-Pakete zu installieren  
- Die Webanwendung ohne Debugging zu starten  