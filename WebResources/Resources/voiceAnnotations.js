function captureVoiceNote(executionCtx){
    Xrm.Device.captureAudio().then(function(file) {
        if(file){
            var objectId = Xrm.Page.data.entity.getId();
            objectId = objectId.replace("{", "").replace("}", "");
            Xrm.WebApi.createRecord("annotation",{
                filename: file.fileName,
                filesize: file.fileSize,
                isdocument: true,
                mimetype: file.mimeType,
                "objectid_opportunity@odata.bind": "/opportunities("+ objectId +")",
                documentbody: fileContent
            })
        }
    }, 
    function(error){
        console.log(error);
    });
}