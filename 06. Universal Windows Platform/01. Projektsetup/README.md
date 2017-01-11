# Modul 6/01 - Entwicklung einer App für die universelle Windows Plattform: Projekteinrichtung und Views

## Übersicht

In diesem Hands-On werden Sie das Projekt aufsetzen und die Hauptseite der App mit XAML erstellen.

_Hinweis: Wir verwenden einige Logos von externen Quellen, mit der freundlichen Unterstützung von_ http://www.alienvalley.com

## Ziele

- Ein Visual Studio Projekt für die universelle Windows Plattform erstellen
- Eine Entwicklerlizenz beantragen
- Das App-Manifest einrichten  
- Die Hauptseite der App mit XAML erstellen
- Die In-App-Navigation einrichten

---

## Übungen

Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Erstellen und Einrichten des Projekts</a><br/>
2. <a href="#Exercise2">Erstellen der Hauptseite der App</a>

<a name="Exercise1"></a>
### Übung 1: Erstellen und Einrichten des Projekts

In dieser Übung werden Sie das Projekt in Visual Studio erstellen und die benötigten Dateien einbinden.

#### Aufgabe 1 - App-Projekt in Visual Studio erstellen

In diesem Schritt wird das Projekt in Visual Studio angelegt und der aktuellen Projektmappe hinzugefügt.

1. In Visual Studio machen Sie einen Rechtsklick auf die aktuell geöffnete Projektmappe **DotNETJumpStart**.
2. Dort wählen Sie **Hinzufügen/Neues Projekt** aus.<br/><br/>
   ![](_images/add-project.png?raw=true "Abbildung 1")<br/>
3. Im Dialog **Neues Projekt**:
   1. Wählen Sie **Vorlagen/Visual C#/Windows/Universell**.
   2. Wählen Sie **Leere App (Universelle Windows-App)**.
   3. Nennen Sie das Projekt **ImageApp** und bestätigen mit **OK**.<br/><br/>
   ![](_images/create-project.png?raw=true "Abbildung 2")<br/>
4. Wählen Sie folgende Einstellungen für die Zielversion in dem sich öffnenden Dialog:<br/><br/>
   ![](_images/version-select.png?raw=true "Abbildung 3")<br/>
   
Falls auf dem System keine Entwicklerlizenz installiert ist, werden Sie nun mit einem Dialog aufgefordert, eine solche zu beantragen. Sollte das nicht der Fall sein, können Sie die nächste Aufgabe überspringen.

#### Aufgabe 2: Abrufen einer Windows Entwickler Lizenz

Wenn Sie zum ersten Mal eine App auf einem Gerät ausführen oder debuggen wollen, werden Sie aufgefordert, eine Entwicklerlizenz für diesen Computer oder dieses Gerät herunterzuladen. Diese ist zum Entwickeln und Testen kostenlos.

1. Lesen Sie sich die Lizenzbedingungen durch und klicken Sie auf die Schaltfläche zum Akzeptieren der Bedingungen<br/><br/>
 ![](_images/license-1.png?raw=true "Abbildung 4")<br/>
2. Klicken Sie im Dialogfeld Benutzerkontensteuerung Control (UAC) auf Ja, um den Vorgang fortzusetzen.
3. Melden Sie sich mit Ihrem Microsoft-Konto an.<br/><br/>
 ![](_images/license-2.png?raw=true "Abbildung 5")<br/>
4. Nachdem Sie die Lizenz auf dem lokalen Computer installiert haben, wird auf diesem Computer erst dann wieder eine entsprechende Benutzeraufforderung eingeblendet, wenn die Lizenz abläuft.<br/><br/>
 ![](_images/license-3.png?raw=true "Abbildung 6")<br/>

#### Aufgabe 3: Startprojekt setzen

Vor den nächsten Schritten muss das neue Projekt noch als **Startprojekt** festgelegt werden.

1. Machen Sie einen Rechtsklick auf das neue Projekt **ImageApp** und wählen **Als Startprojekt festlegen**.
2. Machen Sie einen Rechtsklick auf die Projektmappe **DotNETJumpStart** und öffnen den **Konfigurations-Manager**.
3. Dort wählen Sie bei der **ImageApp** die Optionen "**Erstellen**" und "**Bereitstellen**" an.<br><br>
  ![](_images/config-manager.png?raw=true "Abbildung 7")<br/>
4. Wenn Sie nun das Debugging mit **F5** starten, so wird die App automatisch erstellt und gestartet.

Machen Sie sich nun mit der Projektstruktur vertraut. Folgende Dateien und Ordner sind im Projekt enthalten:
- **Assets/**: Enthält App-Logos.
- **App.xaml**: Die Hauptdatei der App, die für den Startvorgang verantwortlich ist.
- **MainPage.xaml**: Die erste Seite der App, die automatisch bei App-Start aufgerufen wird.
- **.TemporaryKey.pfx**: Temporäres Entwicklerzertifikat
- **Package.appxmanifest**: Die Manifestdatei, in der App-Einstellungen getroffen werden.
- **project.json**: Projektdatei von Visual Studio

#### Aufgabe 4: Mobile Extension hinzufügen

Um gerätespezifische Funktionen auf Desktop-PCs und Smartphones verwenden zu können, müssen noch sogenannte "**Extension SDKs**" hinzugefügt werden. Dieses benötigen wir in einem der späteren Hands-Ons.

1. Hierzu machen Sie einen Rechtsklick auf **Verweise** in der Projektmappe und wählen **Verweis hinzufügen** aus.<br><br>
  ![](_images/verweise.png?raw=true "Abbildung 8")<br/>
2. Im neu geöffneten Fenster wählen Sie links **Universal Windows/Erweiterungen** und selektieren eine auf Ihrem System vorhandene Version vom **Windows Mobile Extensions for the UWP**.<br><br>
  ![](_images/extensions.png?raw=true "Abbildung 9")<br/>
3. Bestätigen Sie mit **OK**.
4. Starten Sie das Debugging. Sollte eine Fehlermeldung angezeigt werden, öffnen Sie erneut die **Verweisübersicht** und wählen eine andere Version des SDKs aus.

#### Aufgabe 5: Manifest einrichten

In dieser Aufgabe werden Sie den App-Namen, Kachelgrafiken, sowie Berechtigungen für die App festlegen.
Diese Einstellungen können im sogenannten **App-Manifest** getroffen werden.

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf den Ordner **Assets** und wählen **Hinzufügen/Vorhandenes Element**.
2. Im Dialogfeld navigieren Sie in den Ordner **Dateien/Assets** vom aktuellen Hands-On und wählen alle Dateien aus.
3. Überschreiben Sie alle bereits vorhandenen Dateien.
4. Machen Sie im **Projektmappen-Explorer** einen Doppelklick auf das **Package.appxmanifest**, um das Manifest zu öffnen.
5. Ändern Sie den Anzeigenamen der App zu einem beliebigen Namen (hier **SnapIt**) und wechseln dann auf den Reiter **Visuelle Assets**.<br/><br/>
   ![](_images/manifest-1.png?raw=true "Abbildung 10")<br/>
6. Wählen Sie links im Menü **Alle Bildanlagen** aus.
7. Wählen Sie in der Kategorie **Kachel** bei der Option **Name anzeigen** aus, dass der App-Name auf dem **Quadratischen 150x150 Logo** angezeigt werden soll.
8. Tragen Sie in der Kategorie **Begrüßungsbildschirm** als Hintergrundfarbe "**white**" ein.<br/><br/>
   ![](_images/manifest-2.png?raw=true "Abbildung 11")<br/><br/>
9. Wechseln Sie nun auf den Reiter **Funktionen** und stellen Sie sicher, dass die Funktion **Internet (Client)** ausgewählt ist.<br/><br/>
   ![](_images/manifest-3.png?raw=true "Abbildung 12")<br/>
10. Wählen Sie **Debugging starten** aus dem Menü **Debuggen** oder drücken Sie **F5**.
11. Die App sollte nun gestartet werden. Sie sollten bereits im Ladebildschirm das eingestellte Logo sehen.
12. Beenden Sie das Debugging und öffnen im Windows Startmenü **Alle Apps**.
13. Dort finden Sie bereits Ihre App mit dem eingestellten Namen.<br/><br/>
   ![](_images/windows-start-menu.png?raw=true "Abbildung 13")<br/>

#### Aufgabe 4: Benötigte Dateien referenzieren
Es wurden für Sie bereits einige Dateien vorbereitet, die häufig in App-Projekten benötigt werden. Auch werden einige Views vorgegeben, um im zeitlichen Rahmen der Veranstaltung zu bleiben. Die Dateien sollen in diesem Schritt in das Projekt eingebunden werden.

1. Erzeugen Sie einen neuen Ordner **Common** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im **Projektmappen-Explorer** tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Fügen Sie daraufhin diesem Ordner die Dateien aus dem aktuellen Hands-On Unterordner **Dateien/Common** hinzu. Hierzu machen Sie einen Rechtsklick auf den neu erstellten Ordner Common und wählen **Hinzufügen/Vorhandenes Element**. 
3. Wiederholen Sie die Schritte 1 und 2 für die Ordner **Views**, **Utils** und **ViewModels**.

Mit diesen Schritten haben Sie die benötigten Dateien für die App eingebunden. Ihre Projektmappe sollte nun wie folgt aussehen:<br/><br/>
   ![](_images/solution-explorer-after-adding.png?raw=true "Abbildung 14")<br/>

<a name="Exercise2"></a>
### Übung 2: Erstellen der Hauptseite der App
In dieser Übung werden Sie die Hauptseite der App mit XAML erstellen. XAML ist eine von Microsoft entwickelte Beschreibungssprache für Oberflächen von Anwendungen, die auf XML basiert.

#### Aufgabe 1 - Anpassen der Hauptseite
Zunächst wird die bereits bestehende Hauptseite der App (**MainPage.xaml**) angepasst und eine App-Bar hinzugefügt.

1. Verschieben Sie die Datei "**MainPage.xaml**" per Drag-and-Drop in den Ordner **Views**.
2. Öffnen Sie die Datei **MainPage.xaml** mit einem Doppelklick im XAML-Designer.
3. Ersetzen Sie den alten Quellcode der Datei durch folgenden XAML-Code:

    ```XML  
	<Page
        x:Class="ImageApp.Views.MainPage"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="using:ImageApp"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"
        xmlns:vm="using:ImageApp.ViewModels"
        xmlns:common="using:ImageApp.Common"
        x:Name="mainPage">

        <!-- TODO: add Page.DataContext here -->

        <!-- TODO: add Page.Resources here -->

        <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}" Margin="0,50,0,0">
            <GridView ItemsSource="{Binding Posts}" 
                      IsItemClickEnabled="False" 
                      SelectionMode="None">
                <GridView.ItemTemplate>
                    <DataTemplate>
                        <Grid Margin="12" Width="320" Height="320">
                            <Grid.RowDefinitions>
                                <RowDefinition Height="70"/>
                                <RowDefinition/>
                                <RowDefinition Height="50"/>
                            </Grid.RowDefinitions>

                            <!-- TODO: add grid template here -->
                        </Grid>
                    </DataTemplate>
                </GridView.ItemTemplate>
            </GridView>

            <!-- TODO: add info label here -->
        </Grid>

        <Page.BottomAppBar>
            <CommandBar>
                <AppBarButton Icon="Camera" Label="Hinzufügen" />
                <AppBarButton Icon="Refresh" Label="Aktualisieren" />

                <CommandBar.SecondaryCommands>
                    <AppBarButton Icon="Sort" Label="Nach Bewertung sortieren" />
                    <AppBarButton Icon="Sort" Label="Nach Datum sortieren" />
                </CommandBar.SecondaryCommands>
            </CommandBar>
        </Page.BottomAppBar>
    </Page>
    ``` 
  
4. Inspizieren Sie den XAML-Code und die daraus resultierenden Steuerelemente im Designer.
5. Starten Sie das Debugging und sehen sich das Ergebnis an.<br/><br/>
 ![](_images/main-page.png?raw=true "Abbildung 15")<br/>

#### Aufgabe 2 - In-App-Navigation hinzufügen
In dieser Aufgabe wird die Navigation von der Hauptseite zur **"Post hinzufügen"**-Seite eingerichtet und die Schaltfläche zum Zurücknavigieren für alle Views aktiviert.

1. Machen Sie im XAML-Designer einen Doppelklick auf den **Hinzufügen-Button** in der App-Bar der Hauptseite.
2. Im neu hinzugefügten **AppBarButton_Click**-Handler fügen Sie den folgenden Code zur Navigation ein:

    ```C#
	this.Frame.Navigate(typeof(AddPostPage));
    ```

3. Starten Sie das Debugging und Testen die Navigation über den Hinzufügen-Button.

Sie werden feststellen, dass Sie keine Möglichkeit haben, zur Hauptseite der App zurückzukehren.
Die Schaltfläche zum Zurücknavigieren muss manuell aktiviert werden. Sie werden hierzu die Schaltfläche global für alle Views aktivieren:

1. Öffnen Sie die Codeansicht der App-Hauptdatei **App.xaml**, indem Sie auf die Datei **App.xaml** rechtsklicken und **Code anzeigen** wählen.
2. Fügen Sie dem using-Block am Anfang der Datei die folgenden Namespace-Verweise hinzu:

    ```C#
	using Windows.UI.Core;
    ```

3. Zunächst aktivieren wir den **AppViewBackButton**. Ersetzen Sie hierzu die Methode **OnLaunched** durch folgende Implementierung:

    ```C#
	protected override void OnLaunched(LaunchActivatedEventArgs e)
     {
        Frame rootFrame = Window.Current.Content as Frame;

        // App-Initialisierung nicht wiederholen, wenn das Fenster bereits Inhalte enthält.
        // Nur sicherstellen, dass das Fenster aktiv ist.
        if (rootFrame == null)
        {
            // Frame erstellen, der als Navigationskontext fungiert und zum Parameter der ersten Seite navigieren
            rootFrame = new Frame();

            rootFrame.NavigationFailed += OnNavigationFailed;

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                //TODO: Zustand von zuvor angehaltener Anwendung laden
            }

            // Den Frame im aktuellen Fenster platzieren
            Window.Current.Content = rootFrame;

            // Register a handler for BackRequested events and set the
            // visibility of the Back button
            rootFrame.Navigated += OnNavigated;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                rootFrame.CanGoBack ?
                AppViewBackButtonVisibility.Visible :
                AppViewBackButtonVisibility.Collapsed;
        }

        if (e.PrelaunchActivated == false)
        {
            if (rootFrame.Content == null)
            {
                // Wenn der Navigationsstapel nicht wiederhergestellt wird, zur ersten Seite navigieren
                // und die neue Seite konfigurieren, indem die erforderlichen Informationen als Navigationsparameter
                // übergeben werden
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // Sicherstellen, dass das aktuelle Fenster aktiv ist
            Window.Current.Activate();
        }
    }
    ```

3. Damit der **AppViewBackButton** auch global verwendet werden kann, fügen Sie der Datei noch folgende 2 Methoden hinzu:

    ```C#
    void OnNavigated(object sender, NavigationEventArgs e)
    {
        // Each time a navigation event occurs, update the Back button's visibility
        SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
            ((Frame)sender).CanGoBack ?
            AppViewBackButtonVisibility.Visible :
            AppViewBackButtonVisibility.Collapsed;
    }

    void OnBackRequested(object sender, BackRequestedEventArgs e)
    {
        Frame rootFrame = Window.Current.Content as Frame;

        if (rootFrame.CanGoBack)
        {
            e.Handled = true;
            rootFrame.GoBack();
        }
    }
    ```
5. Starten Sie die App und versuchen von der **Hinzufügen-Seite** zurück zu navigieren. Es sollte eine neue Schaltfläche angezeigt werden.<br/><br/>
 ![](_images/back-button-app.png?raw=true "Abbildung 16")<br/>

Auf Smartphones wird im Vergleich keine Zurück-Schaltfläche angezeigt. Durch den hinzugefügten Code wird jedoch die Zurück-Taste auf dem Gerät aktiviert.

## Zusammenfassung

Mit Beendung dieser Session haben Sie gelernt:  
- Eine Projekt für die universelle Windows Plattform zu erstellen  
- Eine Windows Entwickler Lizenz abzurufen  
- Das App-Manifest einzurichten  
- Eine XAML-View zu erstellen  
- Die In-App-Navigation zu verwenden  