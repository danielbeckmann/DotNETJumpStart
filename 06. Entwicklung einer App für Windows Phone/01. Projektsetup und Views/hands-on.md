Modul 6/01 - Entwicklung einer App für Windows Phone: Projektsetup und Views
=======================================

##Übersicht
In diesem Hands-On werden Sie das Windows Phone Projekt aufsetzen und die Hauptseite der App mit XAML erstellen.

##Ziele
- Ein Visual Studio Projekt für Windows Phone erstellen
- Eine Entwicklerlizenz beantragen
- Die Hauptseite der App mit XAML erstellen
- Die In-App-Navigation einrichten

##Übungen
Dieses Hands-On besteht aus den folgenden Übungen:<br/>
1. <a href="#Exercise1">Erstellen und Einrichten des Windows Phone Projekts</a><br/>
2. <a href="#Exercise2">Erstellen der Hauptseite der App</a>

<a name="Exercise1"></a>
##Übung 1: Erstellen und Einrichten des Windows Phone Projekts
In dieser Übung werden Sie das Projekt in Visual Studio erstellen und die benötigten Dateien einbinden.

###Aufgabe 1 - Windows Phone Projekt in Visual Studio erstellen
In diesem Schritt wird das Projekt in Visual Studio angelegt.

1. Starten Sie **Visual Studio**.
2. In Visual Studio wählen Sie **Datei/Neu/Projekt**.
3. Im Dialog **Neues Projekt**:
   1. Wählen Sie **Vorlagen/Visual C#/Store-Apps/Windows Phone-Apps**.
   2. Wählen Sie **Leere App (Windows Phone)**.
   3. Nennen Sie das Projekt **ImageApp** und bestätigen mit **OK**.<br/><br/>
   ![](images/create-project.png?raw=true "Abbildung 1")<br/>

Falls auf dem System keine Entwicklerlizenz installiert ist, werden Sie nun in einem Dialog aufgefordert, eine solche zu beantragen. Sollte das nicht der Fall sein, können Sie die nächste Aufgabe überspringen.

###Aufgabe 2: Abrufen einer Windows Entwickler Lizenz
Wenn Sie zum ersten Mal eine App auf einem Gerät ausführen oder debuggen wollen, werden Sie aufgefordert, eine Entwicklerlizenz für diesen Computer oder dieses Gerät herunterzuladen. Diese ist zum Entwickeln und Testen kostenlos.

1. Lesen Sie sich die Lizenzbedingungen durch und klicken Sie auf die Schaltfläche zum Akzeptieren der Bedingungen<br/><br/>
 ![](images/license-1.png?raw=true "Abbildung 2")<br/>
2. Klicken Sie im Dialogfeld Benutzerkontensteuerung Control (UAC) auf Ja, um den Vorgang fortzusetzen.
3. Melden Sie sich mit Ihrem Microsoft-Konto an.<br/><br/>
 ![](images/license-2.png?raw=true "Abbildung 3")<br/>
4. Nachdem Sie die Lizenz auf dem lokalen Computer installiert haben, wird auf diesem Computer erst dann wieder eine entsprechende Benutzeraufforderung eingeblendet, wenn die Lizenz abläuft.<br/><br/>
 ![](images/license-3.png?raw=true "Abbildung 4")<br/>

###Aufgabe 3: Manifest einrichten
In dieser Aufgabe werden Sie den App-Namen, Kachelgrafiken, sowie Berechtigungen für die App festlegen.
Diese Einstellungen können im sogenannten **App-Manifest** getroffen werden.

1. Im **Projektmappen-Explorer** machen Sie einen Rechtsklick auf den Ordner **Assets** und wählen **Hinzufügen/Vorhandenes Element**.
2. Im Dialogfeld navigieren Sie in den Ordner **Files/Assets** vom aktuellen Hands-On und wählen alle Dateien aus.
3. Überschreiben Sie alle bereits vorhandenen Dateien.
4. Machen Sie im **Projektmappen-Explorer** einen Doppelklick auf das **Package.appxmanifest**, um das Manifest zu öffnen.
5. Ändern Sie den Anzeigenamen der App zu **Image App** und wechseln dann auf den Reiter **Visuelle Anlagen**<br/><br/>
   ![](images/manifest-1.png?raw=true "Abbildung 5")<br/>
6. Wählen Sie links im Menü **Alle Bildanlagen** und wählen bei **Kachel** aus, dass der App-Name auf dem **Quadratischen 150x150 Logo** angezeigt werden soll.
7. Überprüfen Sie, ob alle Bilder korrekt gesetzt worden sind. Das **Breite Logo 310x150px** können Sie mit einem Klick auf das **X** entfernen.<br/><br/>
   ![](images/manifest-2.png?raw=true "Abbildung 6")<br/>
7. Wechseln Sie nun auf den Reiter **Funktionen** und stellen Sie sicher, dass die Funktion **Internet (Client und Server)** ausgewählt ist.<br/><br/>
   ![](images/manifest-3.png?raw=true "Abbildung 7")<br/>
8. Wählen Sie **Debugging starten** aus dem Menü **Debuggen** oder drücken Sie **F5**
9. Die App sollte sich nun im Emulator mit einer schwarzen Seite öffnen.
10. Beenden Sie das Debugging und wechseln im Emulator zur App-Übersicht (über eine Wischbewegung nach links oder über die Pfeil-Schaltfläche am unteren Ende der Seite). Die App sollte nun in der Liste mit dem Namen **Image App** angezeigt werden.
11. Halten Sie die Maus mit einem Linksklick auf der App in der Liste gedrückt und wählen **Auf Startseite**.<br/><br/>
   ![](images/add-tile-to-start.png?raw=true "Abbildung 8")<br/>

Die App wird nun auf der Startseite mit der eingestellten Kachel angezeigt.<br/><br/>
   ![](images/app-tile.png?raw=true "Abbildung 9")<br/>

###Aufgabe 4: Benötigte Dateien referenzieren
Es stehen bereits einige Klassen zur Verfügung, die häufig in App-Projekten benötigt werden. Auch werden einige Views vorgegeben, um im zeitlichen Rahmen der Veranstaltung zu bleiben. Die Dateien sollen in diesem Schritt in das Projekt eingebunden werden.

1. Erzeugen Sie einen neuen Ordner **Common** im aktuellen Projekt. Sie können das über einen Rechtsklick auf das Projekt im **Projektmappen-Explorer** tun, indem Sie dort **Hinzufügen/Neuer Ordner** wählen.
2. Fügen Sie daraufhin diesem Ordner die Dateien aus dem aktuellen Hands-On Unterordner **Files/Common** hinzu. Hierzu machen Sie einen Rechtsklick auf den neu erstellten Ordner Common und wählen **Hinzufügen/Vorhandenes Element**. 
3. Wiederholen Sie die Schritte 1 und 2 für die Ordner **Views**, **Services** und **ViewModels**.

Mit diesen Schritten haben Sie die benötigten Dateien für die App eingebunden.

<a name="Exercise2"></a>
##Übung 2: Erstellen der Hauptseite der App
In dieser Übung werden Sie die Hauptseite der App mit XAML erstellen.

###Aufgabe 1 - Anpassen der Hauptseite
In diesem Schritt wird die Hauptseite für die App angepasst. Hierzu wird die bereits bestehende Seite **MainPage.xaml** verwendet.

1. Verschieben Sie die Datei "MainPage.xaml" per Drag-and-Drop in den Ordner **Views**.
2. Öffnen Sie die Datei **MainPage.xaml** mit einem Doppelklick im XAML-Designer.
3. Ersetzen Sie den alten Quellcode der Datei durch folgenden XAML-Code:

    ```XML  
	<Page
		x:Class="ImageApp.MainPage"
		xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
		xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
		xmlns:local="using:ImageApp"
		xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
		xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
		mc:Ignorable="d"
		Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

		<!-- DataContext hier einfügen -->
		
		<Grid>
			<Grid.RowDefinitions>
				<RowDefinition Height="Auto"/>
				<RowDefinition Height="*"/>
			</Grid.RowDefinitions>
			
			<StackPanel Orientation="Horizontal" HorizontalAlignment="Center">
				<TextBlock Text="Sortieren nach:" Style="{StaticResource BaseTextBlockStyle}" VerticalAlignment="Center" />
				<Button Content="Bewertung" Margin="12,0,0,0"/>
				<Button Content="Datum" Margin="12,0,0,0" />
			</StackPanel>

			<Pivot x:Name="PostPivot" Grid.Row="1" Margin="0,27,0,0">
				<Pivot.HeaderTemplate>
					<DataTemplate>
						<!-- Post Titel hier einfügen -->
					</DataTemplate>
				</Pivot.HeaderTemplate>
				<Pivot.ItemTemplate>
					<DataTemplate>
						<!-- Image hier einfügen -->
					</DataTemplate>
				</Pivot.ItemTemplate>
			</Pivot>
		</Grid>

		<Page.BottomAppBar>
			<CommandBar x:Name="CommandBar">
				<AppBarButton Icon="Like" Label="Like"/>
				<AppBarButton Icon="Add" Label="Hinzufügen" />
				<AppBarButton Icon="Refresh" Label="Refresh" />
			</CommandBar>
		</Page.BottomAppBar>
	</Page>
    ``` 
  
4. Inspizieren Sie den XAML-Code und die daraus resultierenden Steuerelemente im Designer.<br/><br/>
 ![](images/main-page.png?raw=true "Abbildung 10")<br/>
5. Starten Sie das Debugging und sehen sich das Ergebnis an.

###Aufgabe 2 - In-App-Navigation hinzufügen
In dieser Aufgabe wird die Navigation von der Hauptseite zur "Post hinzufügen"-Seite hinzugefügt.

1. Machen Sie im XAML-Designer einen Doppelklick auf den **Hinzufügen-Button** in der App-Bar der Hauptseite.
2. Im neu hinzugefügten **OnClick-Handler** fügen Sie den folgenden Code zur Navigation ein:

    ```C#
	this.Frame.Navigate(typeof(AddPostPage));
    ```

3. Starten Sie das Debugging und Testen die Navigation über den Hinzufügen-Button. Versuchen Sie über den Zurück-Pfeil des Emulators auf die Hauptseite zurückzukehren. 

Standardmäßig schließt sich hierbei die App. Der Code zum Zurücknavigieren über den Hardware-Button muss manuell hinzugefügt werden.

1. Öffnen Sie hierzu die Codeansicht der **AddPostPage**, indem Sie die Datei **AddPostPage.xaml.cs** doppelklicken, oder auf die Seite **AddPostPage.xaml** rechtsklicken und **Code anzeigen** wählen.
2. Fügen Sie den folgenden Code in die Seite ein, um den Hardware-Back-Button für diese Seite zu aktivieren:

    ```C#
	protected override void OnNavigatedTo(NavigationEventArgs e)
	{
		HardwareButtons.BackPressed += this.HardwareButtons_BackPressed;
	}

	protected override void OnNavigatedFrom(NavigationEventArgs e)
	{
		HardwareButtons.BackPressed -= this.HardwareButtons_BackPressed;
	}

	private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
	{
		Frame frame = Window.Current.Content as Frame;
		if (frame == null)
		{
			return;
		}

		if (frame.CanGoBack)
		{
			frame.GoBack();
			e.Handled = true;
		}
	}
    ```

3. Testen Sie die In-App-Navigation erneut. Diese sollte nun wie gewünscht funktionieren.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Ein Windows Phone App Projekt zu erstellen  
- Eine Windows Entwickler Lizenz abzurufen  
- Das App-Manifest einzurichten  
- Eine XAML-View zu erstellen  
- Die In-App-Navigation zu verwenden  