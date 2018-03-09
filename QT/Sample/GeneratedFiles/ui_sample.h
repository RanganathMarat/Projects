/********************************************************************************
** Form generated from reading UI file 'sample.ui'
**
** Created by: Qt User Interface Compiler version 5.4.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_SAMPLE_H
#define UI_SAMPLE_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QCheckBox>
#include <QtWidgets/QFrame>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QProgressBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QRadioButton>
#include <QtWidgets/QTabWidget>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QTreeWidget>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_SampleClass
{
public:
    QWidget *centralWidget;
    QPushButton *pushButton;
    QPushButton *pushButton_2;
    QPushButton *pushButton_3;
    QPushButton *pushButton_4;
    QRadioButton *radioButton;
    QRadioButton *radioButton_2;
    QCheckBox *checkBox;
    QCheckBox *checkBox_2;
    QTabWidget *tabWidget;
    QWidget *tab;
    QWidget *tab_2;
    QProgressBar *progressBar;
    QLabel *label;
    QLabel *label_2;
    QLabel *label_3;
    QLabel *label_4;
    QRadioButton *radioButton_3;
    QRadioButton *radioButton_4;
    QFrame *frame;
    QTreeWidget *treeWidget;
    QToolBar *toolBar;

    void setupUi(QMainWindow *SampleClass)
    {
        if (SampleClass->objectName().isEmpty())
            SampleClass->setObjectName(QStringLiteral("SampleClass"));
        SampleClass->setWindowModality(Qt::ApplicationModal);
        SampleClass->resize(1085, 370);
        centralWidget = new QWidget(SampleClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        pushButton = new QPushButton(centralWidget);
        pushButton->setObjectName(QStringLiteral("pushButton"));
        pushButton->setGeometry(QRect(20, 320, 75, 23));
        pushButton_2 = new QPushButton(centralWidget);
        pushButton_2->setObjectName(QStringLiteral("pushButton_2"));
        pushButton_2->setGeometry(QRect(130, 320, 75, 23));
        pushButton_3 = new QPushButton(centralWidget);
        pushButton_3->setObjectName(QStringLiteral("pushButton_3"));
        pushButton_3->setGeometry(QRect(250, 320, 75, 23));
        pushButton_4 = new QPushButton(centralWidget);
        pushButton_4->setObjectName(QStringLiteral("pushButton_4"));
        pushButton_4->setGeometry(QRect(380, 320, 75, 23));
        radioButton = new QRadioButton(centralWidget);
        radioButton->setObjectName(QStringLiteral("radioButton"));
        radioButton->setGeometry(QRect(20, 290, 82, 17));
        radioButton_2 = new QRadioButton(centralWidget);
        radioButton_2->setObjectName(QStringLiteral("radioButton_2"));
        radioButton_2->setGeometry(QRect(20, 260, 82, 17));
        checkBox = new QCheckBox(centralWidget);
        checkBox->setObjectName(QStringLiteral("checkBox"));
        checkBox->setGeometry(QRect(130, 290, 70, 17));
        checkBox_2 = new QCheckBox(centralWidget);
        checkBox_2->setObjectName(QStringLiteral("checkBox_2"));
        checkBox_2->setGeometry(QRect(130, 260, 70, 17));
        tabWidget = new QTabWidget(centralWidget);
        tabWidget->setObjectName(QStringLiteral("tabWidget"));
        tabWidget->setGeometry(QRect(10, 10, 201, 221));
        tab = new QWidget();
        tab->setObjectName(QStringLiteral("tab"));
        tabWidget->addTab(tab, QString());
        tab_2 = new QWidget();
        tab_2->setObjectName(QStringLiteral("tab_2"));
        tabWidget->addTab(tab_2, QString());
        progressBar = new QProgressBar(centralWidget);
        progressBar->setObjectName(QStringLiteral("progressBar"));
        progressBar->setGeometry(QRect(280, 270, 118, 23));
        progressBar->setValue(24);
        label = new QLabel(centralWidget);
        label->setObjectName(QStringLiteral("label"));
        label->setGeometry(QRect(290, 50, 47, 13));
        label_2 = new QLabel(centralWidget);
        label_2->setObjectName(QStringLiteral("label_2"));
        label_2->setGeometry(QRect(290, 90, 47, 13));
        label_3 = new QLabel(centralWidget);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setGeometry(QRect(290, 130, 47, 13));
        label_4 = new QLabel(centralWidget);
        label_4->setObjectName(QStringLiteral("label_4"));
        label_4->setGeometry(QRect(290, 170, 47, 13));
        radioButton_3 = new QRadioButton(centralWidget);
        radioButton_3->setObjectName(QStringLiteral("radioButton_3"));
        radioButton_3->setGeometry(QRect(20, 230, 82, 17));
        radioButton_4 = new QRadioButton(centralWidget);
        radioButton_4->setObjectName(QStringLiteral("radioButton_4"));
        radioButton_4->setGeometry(QRect(20, 280, 82, 17));
        frame = new QFrame(centralWidget);
        frame->setObjectName(QStringLiteral("frame"));
        frame->setGeometry(QRect(590, 30, 451, 291));
        frame->setFrameShape(QFrame::StyledPanel);
        frame->setFrameShadow(QFrame::Raised);
        treeWidget = new QTreeWidget(frame);
        treeWidget->setObjectName(QStringLiteral("treeWidget"));
        treeWidget->setGeometry(QRect(0, 0, 451, 291));
        SampleClass->setCentralWidget(centralWidget);
        toolBar = new QToolBar(SampleClass);
        toolBar->setObjectName(QStringLiteral("toolBar"));
        SampleClass->addToolBar(Qt::TopToolBarArea, toolBar);

        retranslateUi(SampleClass);

        tabWidget->setCurrentIndex(1);


        QMetaObject::connectSlotsByName(SampleClass);
    } // setupUi

    void retranslateUi(QMainWindow *SampleClass)
    {
        SampleClass->setWindowTitle(QApplication::translate("SampleClass", "Sample", 0));
        pushButton->setText(QApplication::translate("SampleClass", "PushButton", 0));
        pushButton_2->setText(QApplication::translate("SampleClass", "PushButton", 0));
        pushButton_3->setText(QApplication::translate("SampleClass", "PushButton", 0));
        pushButton_4->setText(QApplication::translate("SampleClass", "PushButton", 0));
        radioButton->setText(QApplication::translate("SampleClass", "RadioButton", 0));
        radioButton_2->setText(QApplication::translate("SampleClass", "RadioButton", 0));
        checkBox->setText(QApplication::translate("SampleClass", "CheckBox", 0));
        checkBox_2->setText(QApplication::translate("SampleClass", "CheckBox", 0));
        tabWidget->setTabText(tabWidget->indexOf(tab), QApplication::translate("SampleClass", "Tab 1", 0));
        tabWidget->setTabText(tabWidget->indexOf(tab_2), QApplication::translate("SampleClass", "Tab 2", 0));
        label->setText(QApplication::translate("SampleClass", "Label1", 0));
        label_2->setText(QApplication::translate("SampleClass", "label2", 0));
        label_3->setText(QApplication::translate("SampleClass", "label2", 0));
        label_4->setText(QApplication::translate("SampleClass", "label2", 0));
        radioButton_3->setText(QApplication::translate("SampleClass", "RadioButton", 0));
        radioButton_4->setText(QApplication::translate("SampleClass", "RadioButton", 0));
        toolBar->setWindowTitle(QApplication::translate("SampleClass", "toolBar", 0));
    } // retranslateUi

};

namespace Ui {
    class SampleClass: public Ui_SampleClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SAMPLE_H
