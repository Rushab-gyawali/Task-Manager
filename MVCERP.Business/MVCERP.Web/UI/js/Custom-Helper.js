function ManageMessage(code, msg) {
    switch (code) {
    case 0:    
        Success(msg);
        break;
    case 1:
        Fail(msg);
        break;
    case 2:
        Info(msg);
        break;
    default:
        Warning(msg);
    }
}



function Success(msg) {
    iziToast.success({
        title: 'Success',
        message: msg,
        icon: 'mdi mdi-checkbox-multiple-marked-circle',
        position: 'topRight',
        animateInside: true,
        transitionIn: 'fadeInDown',
        titleColor: '#FFF',
        messageColor: '#FFF',
        backgroundColor: '#4CAF50',
        iconColor:"#F9F9F9",
        timeout: 2000
    });
}


function Info(msg) {
    iziToast.info({
        title: 'Info',
        message: msg,
        icon: 'mdi mdi-checkbox-multiple-marked-circle',
        position: 'topRight',
        animateInside: true,
        transitionIn: 'fadeInDown',
        titleColor: '#FFF',
        messageColor: '#FFF',
        backgroundColor: '#2196F3',
        iconColor: "#F9F9F9",
        timeout: 2000
    });
}


function Warning(msg) {
    iziToast.warning({
        title: 'Warning',
        message: msg,
        icon: 'mdi mdi-close-octagon-outline',
        position: 'topRight',
        animateInside: true,
        transitionIn: 'fadeInDown',
        titleColor: '#FFF',
        messageColor: '#FFF',
        backgroundColor: '#FFAB00',
        iconColor: "#F9F9F9",
        timeout: 2000
    });
}


function Fail(msg) {
    iziToast.info({
        title: 'Error',
        message: msg,
        icon: 'mdi mdi-close-octagon-outline',
        position: 'topRight',
        animateInside: true,
        transitionIn: 'fadeInDown',
        titleColor: '#FFF',
        messageColor: '#FFF',
        backgroundColor: '#F44336',
        iconColor: "#F9F9F9",
        timeout: 2000
    });
}




function CharacterValidForIdNo(nField, fieldName) {
    var userInput = nField.value;
    if (userInput == "" || userInput == undefined) {
        return;
    }

    if (/^[a-zA-Z0-9- ._&/\\()]*$/.test(userInput) == false) {
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
        ManageMessage(1, "Only Numbers(e.g. 0-9) are allowed.");
        setTimeout(function () { nField.focus(); }, 1);
    }
}
function validateEmail(nField) {
    var email = nField.value;
    if (email == "" || email == undefined) {
        return;
    }
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (re.test(email) == false) {
        ManageMessage(1, "Enter valid Email Id.");
        setTimeout(function () { nField.focus(); }, 1);
    }
}

function GoBack() {
    window.history.go(-1);
}



function NepaliTranslation(id) {
    google.load("elements", "1", { packages: "transliteration" });
    function onLoad() {
        var options = {
            sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
            destinationLanguage: [google.elements.transliteration.LanguageCode.NEPALI],
            shortcutKey: 'ctrl+g',
            transliterationEnabled: true
        };
        var control =
            new google.elements.transliteration.TransliterationControl(options);

        $('.translate-nepali').each(function () {
            var id = this.id;
            control.makeTransliteratable([id]);
        })
    }
    google.setOnLoadCallback(onLoad);
}