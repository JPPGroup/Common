function getFileSize(fileInput) {
    var file = document.getElementById(fileInput).files[0]; 
    return file.size;    
};

function getFileName(fileInput) {
    var file = document.getElementById(fileInput).files[0];
    return file.name;
};

async function getSlice(fileInput, start, end) {
    return new Promise((resolve, reject) => {      
    

    var file = document.getElementById(fileInput).files[0];   
    var blob = file.slice(start, start + end);

    var reader = new FileReader();

    reader.onloadend = function (e) {
        resolve(e.target.result);
    };

     reader.readAsDataURL(blob);        

    
    });
};

function clearInput(fileInput) {
    document.getElementById(fileInput).value = '';
};

/*function saveAsFile(filename, bytesBase64) {
    if (navigator.msSaveBlob) {
        //Download document in Edge browser
        var data = window.atob(bytesBase64);
        var bytes = new Uint8Array(data.length);
        for (var i = 0; i < data.length; i++) {
            bytes[i] = data.charCodeAt(i);
        }
        var blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
        navigator.msSaveBlob(blob, filename);
    }
    else {
        var link = document.createElement('a');
        link.download = filename;
        link.href = "data:application/octet-stream;base64," + bytesBase64;
        document.body.appendChild(link); // Needed for Firefox
        link.click();
        document.body.removeChild(link);
    
}*/

function saveAsFile(filename) {
    /*if (navigator.msSaveBlob) {
        //Download document in Edge browser
        var data = window.atob(bytesBase64);
        var bytes = new Uint8Array(data.length);
        for (var i = 0; i < data.length; i++) {
            bytes[i] = data.charCodeAt(i);
        }
        var blob = new Blob([bytes.buffer], { type: "application/octet-stream" });
        navigator.msSaveBlob(blob, filename);
    }
    else {*/
        var link = document.createElement('a');
    link.href = "api/download?name=" + filename;
    link.download = filename;
        document.body.appendChild(link); // Needed for Firefox
        link.click();
        document.body.removeChild(link);

}
