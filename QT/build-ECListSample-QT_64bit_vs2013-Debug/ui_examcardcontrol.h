/********************************************************************************
** Form generated from reading UI file 'examcardcontrol.ui'
**
** Created by: Qt User Interface Compiler version 5.4.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_EXAMCARDCONTROL_H
#define UI_EXAMCARDCONTROL_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QFrame>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QListWidget>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QVBoxLayout>

QT_BEGIN_NAMESPACE

class Ui_ExamCardControl
{
public:
    QVBoxLayout *verticalLayout;
    QLabel *scanTime;
    QLabel *examCardName;
    QListWidget *listWidget;
    QPushButton *pushButton;
    QLabel *timeLabel;

    void setupUi(QFrame *ExamCardControl)
    {
        if (ExamCardControl->objectName().isEmpty())
            ExamCardControl->setObjectName(QStringLiteral("ExamCardControl"));
        ExamCardControl->resize(935, 161);
        QSizePolicy sizePolicy(QSizePolicy::Expanding, QSizePolicy::Expanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(ExamCardControl->sizePolicy().hasHeightForWidth());
        ExamCardControl->setSizePolicy(sizePolicy);
        ExamCardControl->setStyleSheet(QLatin1String("#ExamCardControl\n"
"{\n"
"background: light gray\n"
"}"));
        ExamCardControl->setFrameShape(QFrame::Panel);
        ExamCardControl->setFrameShadow(QFrame::Raised);
        verticalLayout = new QVBoxLayout(ExamCardControl);
        verticalLayout->setObjectName(QStringLiteral("verticalLayout"));
        scanTime = new QLabel(ExamCardControl);
        scanTime->setObjectName(QStringLiteral("scanTime"));

        verticalLayout->addWidget(scanTime);

        examCardName = new QLabel(ExamCardControl);
        examCardName->setObjectName(QStringLiteral("examCardName"));

        verticalLayout->addWidget(examCardName);

        listWidget = new QListWidget(ExamCardControl);
        listWidget->setObjectName(QStringLiteral("listWidget"));
        listWidget->setFrameShape(QFrame::StyledPanel);
        listWidget->setSizeAdjustPolicy(QAbstractScrollArea::AdjustToContentsOnFirstShow);
        listWidget->setFlow(QListView::LeftToRight);

        verticalLayout->addWidget(listWidget);

        pushButton = new QPushButton(ExamCardControl);
        pushButton->setObjectName(QStringLiteral("pushButton"));

        verticalLayout->addWidget(pushButton);

        timeLabel = new QLabel(ExamCardControl);
        timeLabel->setObjectName(QStringLiteral("timeLabel"));

        verticalLayout->addWidget(timeLabel);


        retranslateUi(ExamCardControl);

        QMetaObject::connectSlotsByName(ExamCardControl);
    } // setupUi

    void retranslateUi(QFrame *ExamCardControl)
    {
        ExamCardControl->setWindowTitle(QApplication::translate("ExamCardControl", "Frame", 0));
        scanTime->setText(QApplication::translate("ExamCardControl", "00:00:40", 0));
        examCardName->setText(QApplication::translate("ExamCardControl", "Examcard Name", 0));
        pushButton->setText(QApplication::translate("ExamCardControl", "+", 0));
        timeLabel->setText(QApplication::translate("ExamCardControl", "TextLabel", 0));
    } // retranslateUi

};

namespace Ui {
    class ExamCardControl: public Ui_ExamCardControl {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_EXAMCARDCONTROL_H
