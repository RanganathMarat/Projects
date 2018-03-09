#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "QListView.h"
#include <QStringListModel>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    ui->listWidget->hide();

    //QAbstractItemModel* list = new QStringListModel(names);
    ui->listWidget->addItems(QStringList()
                            << "John Doe, Harmony Enterprises, 12 Lakeside, Ambleton"
                            << "Jane Doe, Memorabilia, 23 Watersedge, Beaton"
                            << "Tammy Shea, Tiblanka, 38 Sea Views, Carlton"
                            << "Tim Sheen, Caraba Gifts, 48 Ocean Way, Deal"
                            << "Sol Harvey, Chicos Coffee, 53 New Springs, Eccleston"
                            << "Sally Hobart, Tiroli Tea, 67 Long River, Fedula");

    QStringList numbers;
    numbers << "One" << "Two" << "Three" << "Four" << "Five";

    QAbstractItemModel *model = new QStringListModel(numbers);
    ui->listView->setModel(model);


}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_pushButton_clicked()
{
    close();
}

void MainWindow::on_pushButton_2_clicked()
{
  ui->listWidget->show();
}
