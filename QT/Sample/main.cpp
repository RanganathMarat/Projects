#include "sample.h"
#include <QtWidgets/QApplication>
#include <QtWidgets/QFileSystemModel>
#include <QtWidgets/QTreeView>
#include <QtWidgets\qlistwidget>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);

	//QFileSystemModel model;
	//model.setRootPath("");
	//QTreeView tree;
	//QListWidget 
	//tree.setModel(&model);
	//tree.setRootIndex(model.index(QDir::currentPath()));

	//// Demonstrating look and feel features
	//tree.setAnimated(false);
	//tree.setIndentation(20);
	//tree.setSortingEnabled(true);

	//tree.setWindowTitle(QObject::tr("Dir View"));
	//tree.resize(1920, 1280);
	//tree.show();


	// Trying using a String list view

	
	//Sample w;
	//w.show();
	return a.exec();
}
