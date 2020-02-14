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
