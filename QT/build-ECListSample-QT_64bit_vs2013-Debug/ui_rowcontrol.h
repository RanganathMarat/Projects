/********************************************************************************
** Form generated from reading UI file 'rowcontrol.ui'
**
** Created by: Qt User Interface Compiler version 5.4.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_ROWCONTROL_H
#define UI_ROWCONTROL_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QFrame>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>

QT_BEGIN_NAMESPACE

class Ui_RowControl
{
public:
    QLabel *scanNr;
    QLabel *label_2;
    QLabel *label_3;
    QLabel *label_4;
    QComboBox *comboBox;
    QLabel *label_5;
    QComboBox *comboBox_2;
    QLabel *label_7;
    QLabel *label_8;

    void setupUi(QFrame *RowControl)
    {
        if (RowControl->objectName().isEmpty())
            RowControl->setObjectName(QStringLiteral("RowControl"));
        RowControl->resize(193, 58);
        RowControl->setStyleSheet(QLatin1String("#RowControl {\n"
"color: black;\n"
"border: 2px solid #555;\n"
"border-radius: 11px;\n"
"padding: 5px;\n"
"background:white;\n"
"min-width: 80px;\n"
"}"));
        RowControl->setFrameShape(QFrame::StyledPanel);
        RowControl->setFrameShadow(QFrame::Raised);
        scanNr = new QLabel(RowControl);
        scanNr->setObjectName(QStringLiteral("scanNr"));
        scanNr->setGeometry(QRect(10, 10, 47, 13));
        label_2 = new QLabel(RowControl);
        label_2->setObjectName(QStringLiteral("label_2"));
        label_2->setGeometry(QRect(10, 30, 47, 13));
        label_3 = new QLabel(RowControl);
        label_3->setObjectName(QStringLiteral("label_3"));
        label_3->setGeometry(QRect(140, 30, 31, 16));
        label_4 = new QLabel(RowControl);
        label_4->setObjectName(QStringLiteral("label_4"));
        label_4->setGeometry(QRect(150, 0, 21, 16));
        label_4->setPixmap(QPixmap(QString::fromUtf8(":/new/icons/icon.general.scan.breathhold_c.ico")));
        comboBox = new QComboBox(RowControl);
        comboBox->setObjectName(QStringLiteral("comboBox"));
        comboBox->setGeometry(QRect(50, 30, 41, 20));
        QSizePolicy sizePolicy(QSizePolicy::MinimumExpanding, QSizePolicy::MinimumExpanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(comboBox->sizePolicy().hasHeightForWidth());
        comboBox->setSizePolicy(sizePolicy);
        comboBox->setStyleSheet(QLatin1String("QComboBox:editable,\n"
"QLineEdit,\n"
"QListView {\n"
"    color: rgb(127, 0, 63);\n"
"    background-color: rgb(255, 255, 241);\n"
"    selection-color: white;                                   \n"
"    selection-background-color: rgb(191, 31, 127);\n"
"    border: 2px groove gray;\n"
"    border-radius: 10px;\n"
"    padding: 2px 4px;\n"
"}\n"
"QComboBox QAbstractItemView {\n"
"     border: 2px solid darkgray;\n"
"     selection-background-color: lightgray;\n"
" }"));
        label_5 = new QLabel(RowControl);
        label_5->setObjectName(QStringLiteral("label_5"));
        label_5->setGeometry(QRect(130, 0, 16, 16));
        label_5->setPixmap(QPixmap(QString::fromUtf8(":/new/icons/icon.examcard.scan.sar_c_8x16.ico")));
        comboBox_2 = new QComboBox(RowControl);
        comboBox_2->setObjectName(QStringLiteral("comboBox_2"));
        comboBox_2->setGeometry(QRect(90, 30, 41, 20));
        sizePolicy.setHeightForWidth(comboBox_2->sizePolicy().hasHeightForWidth());
        comboBox_2->setSizePolicy(sizePolicy);
        label_7 = new QLabel(RowControl);
        label_7->setObjectName(QStringLiteral("label_7"));
        label_7->setGeometry(QRect(110, 0, 21, 16));
        label_7->setPixmap(QPixmap(QString::fromUtf8(":/new/icons/general.tablemovement.png")));
        label_8 = new QLabel(RowControl);
        label_8->setObjectName(QStringLiteral("label_8"));
        label_8->setGeometry(QRect(50, 10, 47, 13));

        retranslateUi(RowControl);

        QMetaObject::connectSlotsByName(RowControl);
    } // setupUi

    void retranslateUi(QFrame *RowControl)
    {
        RowControl->setWindowTitle(QApplication::translate("RowControl", "Frame", 0));
        scanNr->setText(QApplication::translate("RowControl", "1,2", 0));
        label_2->setText(QApplication::translate("RowControl", "Geo1", 0));
        label_3->setText(QApplication::translate("RowControl", "00:01", 0));
        label_4->setText(QString());
        comboBox->clear();
        comboBox->insertItems(0, QStringList()
         << QApplication::translate("RowControl", "A", 0)
         << QApplication::translate("RowControl", "B", 0)
         << QApplication::translate("RowControl", "C", 0)
         << QApplication::translate("RowControl", "D", 0)
        );
        label_5->setText(QString());
        comboBox_2->clear();
        comboBox_2->insertItems(0, QStringList()
         << QApplication::translate("RowControl", "L", 0)
         << QApplication::translate("RowControl", "B", 0)
         << QApplication::translate("RowControl", "R", 0)
        );
        label_7->setText(QString());
        label_8->setText(QApplication::translate("RowControl", "Survey", 0));
    } // retranslateUi

};

namespace Ui {
    class RowControl: public Ui_RowControl {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_ROWCONTROL_H
