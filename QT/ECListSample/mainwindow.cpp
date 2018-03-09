#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "examcardcontrol.h"
#include "qlistwidget.h"
#include "samplecontrol.h"

  QListWidget *list;

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
     list  = new QListWidget();
     list->setFlow(QListWidget::TopToBottom);
     ui->scrollArea->setWidget(list);
     ui->scrollArea->setAlignment(Qt::AlignTop);
     ui->scrollArea->setBackgroundRole(QPalette::Dark);
     showMaximized();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_addButton_clicked()
{
    QListWidgetItem *item=new QListWidgetItem(list);
    ExamCardControl *control=new ExamCardControl(list);
    control->setFixedWidth(list->width());
    list->setItemWidget(item,control);
    item->setSizeHint(control->maximumSize());
    list->showMaximized();
}
void MainWindow::showMaximized()
{
    // ...
    setWindowState((windowState() & ~(Qt::WindowMinimized | Qt::WindowFullScreen))
                   | Qt::WindowMaximized);

}
