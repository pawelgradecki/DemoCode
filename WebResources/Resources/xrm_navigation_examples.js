function onLoad() {
    debugger;
    //showAlertDialog();
    //showConfirmDialog();
    //showErrorDialog();

    //openFile();

    //openEntityForm();
    //openUrl();
    openWebResource();
}

function showAlertDialog() {
    var alertStrings = {
        confirmButtonLabel: "This is text shown on the confirmation button",
        text: "This is text shown on the dialog"
    };
    var alertOptions = {
        height: 200,
        width: 400
    };

    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        result => {
            console.log("Success! The dialog was shown");
        },
        error => {
            concole.log(error.message);
        }
    );
}

function showConfirmDialog() {
    var confirmStrings = {
        cancelButtonLabel: "Cancel button label, by default it's CANCEL",
        confirmButtonLabel: "Confirm button label, by default it's OK",
        text: "Text show on the dialog",
        title: "Title of the confirmation dialog",
        subtitle: "Subtitle of the confirmation dialog"
    };

    var confirmOptions = {
        height: 200,
        width: 450
    };

    Xrm.Navigation.openConfirmDialog(confirmStrings, confirmOptions).then(
        success => {
            if (success.confirmed)
                console.log("Dialog closed using OK button.");
            else
                console.log("Dialog closed using Cancel button or X.");
        },
        error => {
            concole.log(error.message);
        });
}

function showErrorDialog() {

    var errorOptions = {
        details: "This is simply the message that can be downloaded by clickick 'Download log file'",
        errorCode: 666, //this is error code if you want to show some specific one. If you specify invalid code or none, the default error code wouldbe displayed
        message: "Message show in the dialog"
    };

    Xrm.Navigation.openErrorDialog(errorOptions).then(
        success => {
            console.log("Dialog was closed successfully");
        },
        error => {
            console.log(error);
        });
}

function openFile() {
    var file = {
        fileContent: "bXkgc2VjcmV0IGNvbnRlbnQ=", //Contents of the file. 
        fileName: "example.txt", //Name of the file.
        fileSize: 24, //Size of the file in KB.
        mimeType: "text/plain" //MIME type of the file.
    }

    Xrm.Navigation.openFile(file, 2);
}

function openEntityForm() {
    var entityFormOptions = {
        entityName: "account",
        useQuickCreateForm: false
    };

    var formParameters = {
        name: "Sample Account",
        description: "This is example of how you can prepopulate the fields on opened entity"
    };

    Xrm.Navigation.openForm(entityFormOptions, formParameters).then(
        success => {
            console.log(success);
        },
        error => {
            console.log(error);
        });
}

function openUrl() {
    var url = "http://google.com";
    var openUrlOptions = {
        height: 400,
        width: 800
    };

    Xrm.Navigation.openUrl(url, openUrlOptions);
}

function openWebResource() {
    var windowOptions = {
        openInNewWindow: false,
        height: 400,
        width: 400
    };

    Xrm.Navigation.openWebResource("new_mywebresource.html", windowOptions, "someAdditionalParameter");
}