import QtQuick 2.4
import QtQuick.Window 2.2
import QtQuick.Controls 1.2
import QtQuick.Controls.Styles 1.2

Window {
    visible: true
    width:500
    height:500
    color:"gray"

   TextField
   {
       id:textfld
       anchors
       {
           top:parent.top
           left:parent.left
           margins:50

       }
       text:"Custom text"
       style:  TextFieldStyle {
           textColor: "black"
           background: Rectangle {
               //   radius: 2
               //            width: 30
               //            height: 22
               color:"gray"

               border.color: control.hovered?"red":"black"

               border.width: 1
           }
       }
       validator: RegExpValidator { regExp: /[0-9A-Z]+/ }
   }
   CustomLabel
   {
       id:cstmlbl
       anchors
       {
           top:textfld.bottom
           left:parent.left
           margins:50

       }
       text: " Switch to Acquistion"
       // image: "../Icons/general.tablemovement.png"
       image: "../Icons/patientpos_ffs .png"
       tooltip: " Switch to current Acquistion session"


   }
   ListModel {
       id: model

       ListElement {
           image:"../Icons/general.tablemovement .png"
           value: "Geo1"

       }
       ListElement {
           image:"../Icons/icon.examcard.scan.breathhold_c.ico"
           value: "Geo2"
       }


       ListElement {
           image:"../Icons/icon.examcard.scan.sar_c_8x16.ico"
           value: "Geo3"
       }

       ListElement {
          image:"../Icons/icon.examcard.scan.breathhold_c.ico"
           value: "Geo4"
       }



       ListElement {
           image:"../Icons/icon.examcard.scan.breathhold_c.ico"
           value: "Geo5"
       }
   }
   CustomCombo
   {
       anchors
       {
           top:cstmlbl.bottom
           left:parent.left
           margins:50

       }
       id: comboBox


       width: 200
       height:20
       selectedItem: 4
       // initialText: "Geo2"
       maxHeight: 200
       listModel: model
   }
//   TextEdit
//   {
//       anchors
//       {
//           top:comboBox.bottom
//           left:parent.left
//           margins:50

//       }

//       color:"white"
//       id: txtIpt
//       text:"ABCGDs"
//   }
}
