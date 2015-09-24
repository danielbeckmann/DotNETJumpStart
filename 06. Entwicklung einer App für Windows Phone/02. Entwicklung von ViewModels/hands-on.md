Modul 6/02 - Entwicklung einer App f�r Windows Phone: Entwicklung von ViewModels
=======================================

##�bersicht
In diesem Hands-On werden Sie das ViewModel f�r die Hauptseite entwickeln und die Datenbindung einrichten.

##Ziele
- Das Datenmodell der API einbinden
- Das ViewModel f�r die Hauptseite erstellen
- Die Datenbindung einrichten
- Einen IValueConverter verwenden
- Commands verwenden

##�bungen
Dieses Hands-On besteht aus den folgenden �bungen:<br/>
1. <a href="#Exercise1">Einbinden des Datenmodells der API</a><br/>
2. <a href="#Exercise2">Erstellen des ViewModels und Einrichten der Datenbindung</a><br/>
3. <a href="#Exercise3">Verwenden eines IValueConverters</a><br/>
4. <a href="#Exercise4">Erstellen von Commands f�r den Like-Button, die Sortierung und Datenaktualisierung</a><br/>

<a name="Exercise1"></a>
##�bung 1: Einbinden des Datenmodells der API
In dieser �bung werden Sie die Datenklassen der API in das Windows Phone Projekt einbinden.

###Aufgabe 1 - Datenklassen kopieren
Die Klassen des Datenmodells der API enthalten unn�tige Annotationen, die in einem Windows Phone Projekt nicht aufgel�st werden k�nnen. Die Datenklassen wurden davon bereinigt im Ordner **Files/DataModel** des aktuellen Hands-Ons bereitgestellt.

1. Erzeugen Sie einen neuen Ordner **DataModel** im aktuellen Projekt. Sie k�nnen das �ber einen Rechtsklick auf das Projekt im **Projektmappen-Explorer** tun, indem Sie dort **Hinzuf�gen/Neuer Ordner** w�hlen.
2. Machen Sie einen Rechtsklick auf den neu erstellten Ordner **DataModel** und w�hlen **Hinzuf�gen/Vorhandenes Element**. 
3. Im Dialogfeld navigieren Sie in den Ordner **Files/DataModel** aus dem aktuellen Hands-On und w�hlen alle Dateien aus.
4. Die Projektmappe sollte nun wie folgt aussehen:<br/><br/>
   ![](images/datamodel-added.png?raw=true "Abbildung 1")
	
Sie haben nun das Datenmodell der API eingebunden und k�nnen dessen Datentypen verwenden.

<a name="Exercise2"></a>
##�bung 2: Erstellen des ViewModels und Einrichten der Datenbindung
In dieser �bung werden Sie das ViewModel f�r die Hauptseite erstellen und die Datenbindung mithilfe von statischen Testdaten einrichten.

###Aufgabe 1 - ViewModel Klasse erstellen
In diesem Schritt wird die ViewModel Klasse mit Testdaten f�r die Datenbindung erstellt.

1. Erzeugen Sie eine neue Klasse **MainViewModel** im Ordner ViewModels, indem Sie auf den Ordner einen Rechtsklick machen und **Hinzuf�gen/Klasse** w�hlen.<br/><br/>
   ![](images/add-mainviewmodel.png?raw=true "Abbildung 2")
2. F�gen Sie dem using-Block die folgenden Namespace-Verweise hinzu:

    ```C#
	using ImageApp.Common;
	using ImageApp.DataModel;
    ```

3. Annotieren Sie die Klasse wie folgt:

    ```C#
	public class MainViewModel : BindableBase
    ```
	
4. Auf der Hauptseite soll eine Liste der Bilder, bzw. Posts im System angezeigt werden. Hierzu f�gen Sie  dem MainViewModel den folgenden Codeblock hinzu:

    ```C#
	private List<Post> posts;

	/// <summary>
	/// Gets or sets the list of posts.
	/// </summary>
	public List<Post> Posts
	{
		get { return this.posts; }
		set { this.SetProperty(ref this.posts, value); }
	}
    ```
	
5. F�gen Sie der Klasse einen Konstruktor hinzu und laden Sie dort folgende Testdaten:

    ```C#
	this.posts = new List<Post>
	{
		new Post { Title = "Baseball", ImageUri = "http://lorempixel.com/400/300/sports/1" },
		new Post { Title = "Surfing", ImageUri = "http://lorempixel.com/400/300/sports/2" },
		new Post { Title = "Cat", ImageUri = "http://lorempixel.com/400/300/cats/1" },
		new Post { Title = "Another cat", ImageUri = "http://lorempixel.com/400/300/cats/5" }
	};
    ```
	
6. W�hlen Sie im Men� **Erstellen** die Aktion **Projektmappe erstellen** aus.

Sie haben nun das ViewModel f�r die Hauptseite erstellt, das eine Liste von Posts bereitstellt, die nun angezeigt werden sollen. Im n�chsten Schritt wird die Datenbindung auf der Hauptseite eingerichtet.

**C#-Quellcode zum Vergleich**:

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageApp.Common;
using ImageApp.DataModel;

namespace ImageApp.ViewModels
{
	public class MainViewModel : BindableBase
	{
		private List<Post> posts;

		public MainViewModel()
		{
			this.posts = new List<Post>
			{
				new Post { Title = "Baseball", ImageUri = "http://lorempixel.com/400/300/sports/1" },
				new Post { Title = "Surfen", ImageUri = "http://lorempixel.com/400/300/sports/2" },
				new Post { Title = "Katze", ImageUri = "http://lorempixel.com/400/300/cats/1" },
				new Post { Title = "Noch eine Katze", ImageUri = "http://lorempixel.com/400/300/cats/5" }
			};
		}

		/// <summary>
		/// Gets or sets the list of posts.
		/// </summary>
		public List<Post> Posts
		{
			get { return this.posts; }
			set { this.SetProperty(ref this.posts, value); }
		}
	}
}
```
	
###Aufgabe 2 - Datenbindung auf der XAML-View erstellen
In diesem Schritt wird das eben erstellte **MainViewModel** auf der Hauptseite eingebunden und die Datenbindung hergestellt.

1. �ffnen Sie die **MainPage.xaml** im XAML-Designer und f�gen dem **Page-Element**  das folgende Attribut hinzu: **xmlns:vm="using:ImageApp.ViewModels"**
2. F�gen Sie oberhalb des **Grid-Elements** den folgenden Block ein:

    ```XML  
	<Page.DataContext>
		<vm:MainViewModel />
	</Page.DataContext>
    ``` 
  
3. F�gen Sie dem Pivot-Element das folgende Attribut hinzu: **ItemsSource="{Binding Posts}"**
4. Legen Sie als **Pivot.HeaderTemplate** den Titel des aktuellen Posts fest, indem Sie innerhalb des **DataTemplates** das folgende Element einf�gen:

    ```XML  
	<TextBlock Text="{Binding Title}" FontSize="42" />
    ``` 
  
5. Legen Sie als **Pivot.ItemTemplate** das eigentliche Bild des Posts fest, indem Sie innerhalb des **DataTemplates** das folgende Element einf�gen:
 
    ```XML  
	<Image Source="{Binding ImageUri}"/>
    ```
	
6. Starten Sie das Debugging. Die Posts-Auflistung wird nun auf das Pivot gebunden und es werden die Bilder mit ihren Titel im Pivot angezeigt.
7. Inspizieren Sie den Code zur Datenbindung, auch im Vergleich auf die Klasse **Post**.

Mit diesen Schritten haben Sie das ViewModel f�r die Hauptseite mit der ersten Datenbindung erzeugt.

**XAML-Quellcode zum Vergleich**:

```XML  
<Page
	x:Class="ImageApp.MainPage"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	xmlns:vm="using:ImageApp.ViewModels"
	xmlns:local="using:ImageApp"
	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
	mc:Ignorable="d"
	Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">

	<Page.DataContext>
		<vm:MainViewModel />
	</Page.DataContext>

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

		<Pivot x:Name="PostPivot" Grid.Row="1" Margin="0,27,0,0" ItemsSource="{Binding Posts}">
			<Pivot.HeaderTemplate>
				<DataTemplate>
					<TextBlock FontSize="42" Text="{Binding Title}"/>
				</DataTemplate>
			</Pivot.HeaderTemplate>
			<Pivot.ItemTemplate>
				<DataTemplate>
					<Image Source="{Binding ImageUri}"/>
				</DataTemplate>
			</Pivot.ItemTemplate>
		</Pivot>
	</Grid>

	<Page.BottomAppBar>
		<CommandBar x:Name="CommandBar">
			<AppBarButton Icon="Like" Label="Like"/>
			<AppBarButton Icon="Add" Label="Hinzuf�gen" />
		</CommandBar>
	</Page.BottomAppBar>
</Page>
``` 

<a name="Exercise3"></a>
##�bung 3: Verwenden eines IValueConverters
In dieser �bung kommt der **CollectionToVisibilityConverter** aus dem Commons-Ordner auf der Hauptseite zum Einsatz. Er wandelt die Anzahl von Elementen in einer Auflistung (Collection) in einen Sichtbarkeitszustand um.

**Konverterlogik**:
- Elemente in einer Auflistung vorhanden -> sichtbar
- Leere Auflistung -> unsichtbar

Wird ein **ConverterParameter** angegeben, so wird der Sichtbarkeitszustand invertiert.

###Aufgabe 1 - IValueConverter in die Hauptseite einbinden
In diesem Schritt wird der **CollectionToVisibilityConverter** in die Hauptseite eingebunden.

1. �ffnen Sie die Datei **CollectionToVisibilityConverter** im **Projektmappen-Explorer** im Ordner **Common**. Inspizieren Sie den Code, um sich mit der Funktionsweise eines Converters vertraut zu machen.
2. �ffnen Sie die **MainPage.xaml** im XAML-Designer und f�gen dem **Page-Element** das folgende Attribut hinzu: **xmlns:common="using:ImageApp.Common"**
3. F�gen Sie oberhalb des **Grid-Elements** den folgenden Block ein:

    ```XML  
	<Page.Resources>
		<common:CollectionToVisibilityConverter x:Key="CollectionToVisibilityConverter" />
	</Page.Resources>
    ``` 
 
Sie haben mit diesen Schritten der Hauptseite den Konverter �ber den Alias **CollectionToVisibilityConverter** bekannt gemacht.

###Aufgabe 2 - Infomeldung erstellen: "Es sind keine Elemente vorhanden"

1. F�gen Sie unterhalb des **Pivot-Elements** den folgenden TextBlock ein:
	
    ```XML  
	<TextBlock Grid.Row="1" Style="{StaticResource BaseTextBlockStyle}" Margin="24"
                   Text="Es sind keine Elemente vorhanden."
                   Visibility="{Binding Posts, Converter={StaticResource CollectionToVisibilityConverter}, ConverterParameter=1}"/>
    ``` 

2. Starten Sie das Debugging. Der TextBlock sollte nicht angezeigt werden, da Daten vorhanden sind.
3. �ffnen Sie die Datei **MainPageViewModel.cs** und entfernen die Testdaten.
4. Starten Sie das Debugging. Der TextBlock sollte nun angezeigt werden. Das Pivot wird automatisch ausgeblendet, da keine Elemente vorhanden sind. Falls das nicht der Fall w�re, k�nnte genau dieser Konverter ebenfalls zu diesem Zweck verwendet werden.

In dieser �bung haben Sie einen Konverter erstellt, der eine Anzahl von Elementen in einer Auflistung in einen Sichtbarkeitszustand umwandelt und den MVVM-Prinzipien entspricht.
	
<a name="Exercise4"></a>
##�bung 4: Erstellen von Commands f�r den Like-Button, die Sortierung und Datenaktualisierung
In dieser �bung werden Sie mehrere Commands f�r die Like-, Sortierungs-, und Datenaktualisierungs-Funktion auf der Hauptseite erstellen.

###Aufgabe 1 - Commands im ViewModel anlegen
In diesem Schritt werden die Commands im ViewModel angelegt.
	
1. �ffnen Sie die Datei **MainViewModel.cs** in Visual Studio.
2. F�gen Sie der Klasse die Definition der einzelnen Commands hinzu:

    ```C#
	/// <summary>
	/// Gets or sets the command to sort after rating.
	/// </summary>
	public DelegateCommand SortRatingCommand { get; set; }

	/// <summary>
	/// Gets or sets the command to sort after date.
	/// </summary>
	public DelegateCommand SortDateCommand { get; set; }

	/// <summary>
	/// Gets or sets the command to like a post.
	/// </summary>
	public DelegateCommand LikeCommand { get; set; }
	
	/// <summary>
	/// Gets or sets the command to refresh the posts.
	/// </summary>
	public DelegateCommand RefreshCommand { get; set; }
    ```

3. F�gen Sie der Klasse die Implementierung der Command-Logik hinzu:

    ```C#
	private async void Like(object obj)
	{
		var dialog = new Windows.UI.Popups.MessageDialog("Like command works!");
		await dialog.ShowAsync();
	}

	private async void SortByRating(object obj)
	{
		var dialog = new Windows.UI.Popups.MessageDialog("Sort by rating command works!");
		await dialog.ShowAsync();
	}

	private async void SortByDate(object obj)
	{
		var dialog = new Windows.UI.Popups.MessageDialog("Sort by date command works!");
		await dialog.ShowAsync();
	}
	
	private async void RefreshData(object obj)
	{
		var dialog = new Windows.UI.Popups.MessageDialog("Refresh command works!");
		await dialog.ShowAsync();
	}
    ```

4. Um die Commands mit den Methoden zu verbinden, f�gen Sie dem Konstruktor die folgenden Zeilen hinzu:

    ```C#
	this.SortDateCommand = new DelegateCommand(this.SortByDate);
	this.SortRatingCommand = new DelegateCommand(this.SortByRating);
	this.LikeCommand = new DelegateCommand(this.Like);
	this.RefreshCommand = new DelegateCommand(this.RefreshData);
    ```

Sie haben in dieser Aufgabe Commands erstellt, die nun auf der Hauptseite verwendet werden k�nnen. Hierzu muss im n�chsten Schritt die Datenbindung mit XAML aufgebaut werden.

###Aufgabe 2 - Datenbindung f�r Commands festlegen
In diesem Schritt werden die Commands mit der Hauptseite verbunden.

1. �ffnen Sie die **MainPage.xaml** im XAML-Designer.
2. F�gen Sie der Schaltfl�che zum "Sortieren nach Bewertung" das folgende Attribut hinzu: **Command="{Binding SortRatingCommand}"**
3. F�gen Sie der Schaltfl�che zum "Sortieren nach dem Datum" das folgende Attribut hinzu: **Command="{Binding SortDateCommand}"**
4. F�gen Sie der Schaltfl�che zum Liken in der **BottomAppBar** das folgende Attribut hinzu: **Command="{Binding LikeCommand}"**
5. F�gen Sie der Schaltfl�che zum Aktualisieren in der **BottomAppBar** das folgende Attribut hinzu: **Command="{Binding RefreshCommand}"**
6. Starten sie das Debugging und Testen Sie die Schaltfl�chen.

In dieser �bung haben Sie gelernt, wie man Commands verwendet.

##Zusammenfassung
Mit Beendung dieser Session haben Sie gelernt:  
- Ein ViewModel zu erstellen
- Einen IValueConverter zu verwenden
- Commands zu verwenden