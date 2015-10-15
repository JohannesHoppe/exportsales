## Export Sales ##

German description below, since the software focuses the German speaking marked only!


---


## Einführung ##

Die CSV-Export-Funktion von SM unterstüzt nicht Kreditkarten und Sparkonten.
Dies Tool schafft Abhilfe. Es ist in C# / .NET 2.0 geschrieben und verwendet WinAPI-Befehle um auf die Textboxen von StarMoney zugreifen zu können.

Sie können das Programm kostenlos verwenden, es ist Open Source / GPL 3!


![![](http://exportsales.googlecode.com/files/ExportSales_screenshot_small.png)](http://exportsales.googlecode.com/files/ExportSales_screenshot.png)


---


## Details ##

Exportiert werden:
  * Kreditkartenkonten
  * Sparkonten

Folgende Star-Money Versionen wurden erfolgreich getestet:
  * StarMoney 5.0 (Version von 2008 verwenden!)
  * StarMoney 6.0 (neueste Version 2010 verwenden!)

  * **[StarMoney 7.0 funktioniert NICHT!](http://code.google.com/p/exportsales/issues/detail?id=1)**

Ausgabeformate
  * CSV (ähnelt dem Export-Format von Girokonten)
  * SQL (für meine proprietäre Fibu)

Neu:
  * XML-Datei zur permanenten Konfiguration
  * Abspeichern des Output als Textdatei

Support:
http://www.starfinanz.de/forum/viewtopic.php?f=30&t=12261


---


## Anleitung ##

  * StarMoney starten, mein Tool starten.
  * Damit der Export klappen kann, müssen die Angaben bei "Einstellungen" unbedingt mit den Werten in StarMoney übereinstimmen. Weiterhin muss die Detail-Ansicht offen sein. Achten Sie darauf, das alle Einstellungen korrekt sind - ein Tippfehler und es klappt nicht! (Die Angaben können in der XML-Datei hinterlegt werden.) **Es sollte also wie auf dem Screenshot aussehen:**
![![](http://exportsales.googlecode.com/files/ExportSales_usage_small.png)](http://exportsales.googlecode.com/files/ExportSales_usage.png)
  * Die Detail-Ansicht sollte die erste zu exportierend Transaktion anzeigen.
  * Jetzt 1x auf "Auslesen" klicken.
  * Wichtig: Das Feld "Fortlaufender Saldo" wird berechnet, nicht ausgelesen. Wenn der Wert nicht dem Saldo von dieser Transaktion entspricht, muss man jetzt die Zahl korrigieren. Dezimalzeichen ist der PUNKT! --> Erklärung!
  * Danach für jede weitere Transaktion fortlaufend auf "Weiter + Auslesen" klicken.
  * Beim der letzten Transaktion kommt eine Message Box mit dem Hinweis: "Die neuen und die alten Daten sind identisch..." Das ist normal und bedeutet, das wir fertig sind.
  * Zu guter Letzt: auf "Speichern" klicken, Dateiname + Pfad auswählen und abspeichern.

--> Erklärung:
Lautet der Betrag bei der ersten Transaktion "10,00" und im Saldo stand "0.00", so steht danach der Wert "10.00" im Feld "Fortlaufender Saldo". Lautet nach einem Klick auf "Weiter + Auslesen" der nächste Betrag "-15,00", so steht jetzt "-5.00" im Feld "Fortlaufender Saldo". That's it.