
function GoBack() {
    window.history.go(-1);
}
function FilterForm() {
    $("#form").submit();
}
function ValidRequiredField(RequiredField) {
    var Isvalid = true;
    var OtherPersonFld = new Array;
    var fld = RequiredField.split(',');
    for (n = 0; n < fld.length - 1; n++) {
        OtherPersonFld[n] = fld[n];
    }
    for (i = 0; i < OtherPersonFld.length; i++) {
        document.getElementById(OtherPersonFld[i]).style.background = "#FFFFFF";

        if (document.getElementById(OtherPersonFld[i]).value == "") {
            document.getElementById(OtherPersonFld[i]).style.background = "#FF99FF";
            Isvalid = false;
        }

    }
    if (Isvalid == false) {
        alert("Required Field(s)\n _____________________________ \n The red fields are required!")
    }

    return Isvalid;
}
function CharacterValidForIdNo(nField, fieldName) {
    var userInput = nField.value;
    if (userInput == "" || userInput == undefined) {
        return;
    }

    if (/^[a-zA-Z0-9- ._&/\\()]*$/.test(userInput) == false) {
        //var msg = "";
        //if (fieldName == "" || fieldName == undefined || fieldName == "undefined") {
        //    msg = " in field : " + fieldName;
        //}
        //alert('Validation\n _____________________________ \nSpecial Character(e.g. !@#$%^&*-) are not allowed.');
        ManageMessage(1, "Special Character(e.g. !@#$%^&*-) are not allowed.");
        setTimeout(function () { nField.focus(); }, 1);
    }
}

function MakeNumberOnly(nField) {
    var userInput = nField.value;
    if (userInput == "" || userInput == undefined) {
        return;
    }
    if (/^[0-9- ./\\()]*$/.test(userInput) == false) {
        // alert('Validation\n _____________________________ \nOnly Numbers(e.g. 0-9) are allowed.');
        ManageMessage(1, "Only Numberic value allowed.");
        setTimeout(function () { nField.value = ""; nField.focus(); }, 1);
    }
}
function BlockSpecialChar(obj) {
    $(function () {
        $('#'+obj).keyup(function () {
            var yourInput = $(this).val();
            re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
            var isSplChar = re.test(yourInput);
            if (isSplChar) {
                var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
                $(this).val(no_spl_char);
            }
        });
    });
}
function ValidURL(str) {
    var userInput = str.value;
    var pattern = /^(http[s]?:\/\/){0,1}(www\.){0,1}[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,5}[\.]{0,1}/; // fragment locater
    if (!pattern.test(userInput)) {
        ManageMessage(1, "Invalid URL.");
        setTimeout(function () { str.value = ""; str.focus(); }, 1);
    } else {
        return true;
    }
}
function validateEmail(nField) {
    var email = nField.value;
    if (email == "" || email == undefined) {
        return;
    }
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(email) == false) {
        //alert("Validation\n _____________________________ \n Enter valid Email Id");
        ManageMessage(1, "Enter valid Email Id.");
        setTimeout(function () { nField.focus(); }, 1);
    }
}
function ValidateMinMax(min, max) {
    var minVal = parseFloat($("#" + min).val());
    var maxVal = parseFloat($("#" + max).val());

    if (minVal > maxVal) {
        ManageMessage(1, "Maximum value must be greater than " + minVal);
        setTimeout(function () { $("#" + max).focus(); }, 1);
        return;
    }
}
function OpenWindow(url) {
    var browser = navigator.appName;
    if (browser == "Microsoft Internet Explorer") {
        window.opener = self;
    }

    window.open(url, "", "width=900,height=750,toolbar=no,scrollbars=yes,location=no,resizable =yes");
    window.moveTo(0, 0);
    window.resizeTo(screen.width, screen.height - 100);
    self.close();
}
//show uploaded images
function showImage(param) {
    var imgSrc = $(param).attr("src");
    OpenInNewWindow(imgSrc);
};


function OpenInNewWindow(url) {
    window.open(url, "", "width=825,height=500,resizable=1,status=1,toolbar=0,scrollbars=1,center=1");
}
//
function GetDDL(DDLName,DDLClass, selectedValue) {
    var html = "<select id='" + DDLName + "' name='" + DDLName + "' class = 'form-control' >";
    $("." + DDLClass + " option").each(function () {
        if ($(this).val() == selectedValue) {
            html += "<option value='" + $(this).val() + "' selected>" + $(this).text() + "</option>";
        }
        else {
            html += "<option value='" + $(this).val() + "'>" + $(this).text() + "</option>";
        }
    });

    html += "</select>";
    return html;
}