import QtQuick 2.3
import QtQuick.Controls 1.2

Item {
    id:container
    property string text: "CustomLabel"
    property string image
    property string tooltip
    Image {
        id: imagev
        source: container.image
        height:30
        width:30
    }
    MouseArea
    {
        id:mouseArea
        anchors.fill: parent
        hoverEnabled: true
    }


    ToolTip {
             id: toolTip
             text: container.tooltip
             target: label
             width:200

         }

    Label {


        id: label
        text:container.text
        anchors
        {
         //  centerIn:container
            left:imagev.right
            top: imagev.top

            leftMargin:10
        }
    }


}
