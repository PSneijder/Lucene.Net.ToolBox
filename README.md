# Lucene.Net.ToolBox

![alt tag](https://github.com/PSneijder/Lucene.Net.ToolBox/blob/master/Assets/Lucene.Net.ToolBox.png)

Lucene.Net.ToolBox is a handy development and diagnostic tool for Lucene.
This tool is used to demonstrate how different analyzers process text into tokens.
You can edit this text to try different input such as numbers like 23231.23 or characters (this.mail@mailprovider.com).

Once happy, select an Analyzer from the list of analyzers found on the current assemblies path and then hit the Analyze button.
The tokens produced are shown below and when you select them the right panel shows their attributes, and the corresponding span in the original text is highlighted.

![alt tag](https://github.com/PSneijder/Lucene.Net.ToolBox/blob/master/Assets/CodeMap.png)

# TODOs
* Viewing your documents and analyzing their field contents
* Searching in the index
* Performing index maintenance: index sanity checking, index optimization
* Reading index from hdfs
* Exporting the index or portion of it into an xml format
* <strike>Testing your custom Lucene analyzers</strike>
* <strike>Token highlighting</strike>

# Recent Changes
See [CHANGES.txt](CHANGES.txt)

# Committers
* [Philip Schneider](https://github.com/PSneijder)

# Licensing
The license for the code is [ALv2](http://www.apache.org/licenses/LICENSE-2.0.html).
