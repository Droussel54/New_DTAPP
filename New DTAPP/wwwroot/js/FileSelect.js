const preview = document.querySelector('.preview');

function addInputToJsonFileList(files) {
    let fileJsonArray = [];
    const fileList = document.getElementById("fileList").value;

    if (fileList != null && fileList != "") {
        fileJsonArray = JSON.parse(document.getElementById("fileList").value);
    }
    var inc = 0;
    for (const file of files) {
        fileJsonArray.push({ "fileId": inc, "fileName": file.name, "fileExt": file.name.slice(file.name.lastIndexOf(".")+1), "fileSize": returnFileSize(file.size), "fileSent": true, "fileComment": file.fileComment });
        inc++;
    }
    listFiles(fileJsonArray, false);
}

function addManualFiletoJsonFileList(ext, fileSize, unit) {
    console.log("ext "+ext);
    console.log("size "+fileSize);
    if (fileSize != null && fileSize != "" && !isNaN(fileSize) && ext != null && ext != "") {
        fileSize = fileSize + ' ' + unit;
        let fileJsonArray = [];
        const fileList = document.getElementById("fileList").value;

        if (fileList != null && fileList != "") {
            fileJsonArray = JSON.parse(document.getElementById("fileList").value);
        }
        var inc = 0;
        fileJsonArray.push({ "fileId": inc, "fileName": "Manualy Added", "fileExt": ext, "fileSize": fileSize, "fileSent": true, "fileComment": "" });
        listFiles(fileJsonArray, false);
    }
}

function listFiles(jsonFiles, readOnly) {
    let transferSize = 0;
    // remove previous list
    while (preview.firstChild) {
        preview.removeChild(preview.firstChild);
    }
    if (jsonFiles.length === 0) {
        const para = document.createElement('p');
        para.textContent = 'No files';
        preview.appendChild(para);
    } else {

        const table = document.createElement('table');
        table.setAttribute("id", 'file-table');
        preview.appendChild(table);
        let thead = document.createElement('thead');
        let trHead = document.createElement('tr');
        let thNumb = document.createElement('th');
        thNumb.textContent = `#`;
        let thFileName = document.createElement('th');
        thFileName.textContent = `File Name`;
        let thFileExt = document.createElement('th');
        thFileExt.textContent = `File Extension`;
        let thFileSize = document.createElement('th');
        thFileSize.textContent = `File Size`;
        let thFileSent = document.createElement('th');
        thFileSent.textContent = `File Sent`;
        let thFileComment = document.createElement('th');
        thFileComment.textContent = `Comment`;


        trHead.appendChild(thNumb);
        trHead.appendChild(thFileName);
        trHead.appendChild(thFileExt);
        trHead.appendChild(thFileSize);
        trHead.appendChild(thFileSent);
        trHead.appendChild(thFileComment);

        if (!readOnly) {
            let thDelete = document.createElement('th');
            trHead.appendChild(thDelete);
        }

        thead.appendChild(trHead);
        table.appendChild(thead);

        let tbody = document.createElement('tbody');

        var i = 1;
        for (const file of jsonFiles) {

            transferSize += returnBytes(file.fileSize);

            let trItem = document.createElement('tr');

            let tdNum = document.createElement('td');
            file.fileId = i;
            tdNum.textContent = i++;
            trItem.appendChild(tdNum);

            let tdFileName = document.createElement('td');
            tdFileName.textContent = `${file.fileName}`;
            trItem.appendChild(tdFileName);

            let tdFileExt = document.createElement('td');
            tdFileExt.textContent = `${file.fileExt}`;
            trItem.appendChild(tdFileExt);

            let tdFileSize = document.createElement('td');
            tdFileSize.textContent = `${file.fileSize}`;
            trItem.appendChild(tdFileSize);

            if (!readOnly) {

                let sentInput = document.createElement('input');

                sentInput.type = "checkbox";
                sentInput.checked = file.fileSent;

                sentInput.setAttribute("onclick", 'changeSent("' + file.fileName + '")');

                let tdSent = document.createElement('td');
                tdSent.appendChild(sentInput);
                trItem.appendChild(tdSent);
            } else {
                let sentInput = document.createElement('input');

                sentInput.type = "checkbox";
                sentInput.checked = file.fileSent;
                sentInput.disabled = true;

                let tdSent = document.createElement('td');
                tdSent.appendChild(sentInput);
                trItem.appendChild(tdSent);
            }
            if (!readOnly) {

                let commentInput = document.createElement('input');

                commentInput.type = "text";

                // console.log('What is this value? ' + file.fileComment);
                // alert('What is this value? ' + file.fileComment);
                //alert(file.fileComment === null);

                if (file.fileComment === undefined || file.fileComment === null) {
                    commentInput.value = "";
                }
                else {
                    commentInput.value = `${file.fileComment}`;
                }

                //alert(commentInput.value);

                let tdComment = document.createElement('td');

                commentInput.setAttribute("onchange", 'changeCommentId("' + file.fileId + '",this.value)');

                tdComment.appendChild(commentInput);
                trItem.appendChild(tdComment);

            } else {
                let commentInput = document.createElement('input');

                commentInput.type = "text";
                if (file.fileComment === undefined || file.fileComment === null) {
                    commentInput.value = "";
                }
                else {
                    commentInput.value = `${file.fileComment}`;
                }
                commentInput.disabled = true;
                let tdComment = document.createElement('td');
                tdComment.appendChild(commentInput);
                trItem.appendChild(tdComment);
            }

            if (!readOnly) {
                let deleteInput = document.createElement('input');
                deleteInput.type = "image";
                deleteInput.src = "/images/x-circle.svg";
                deleteInput.setAttribute("onclick", 'removeFileFromList("' + file.fileId + '")');

                let tdDelete = document.createElement('td');
                tdDelete.appendChild(deleteInput);
                trItem.appendChild(tdDelete);
            }
            tbody.appendChild(trItem);
        }
        table.appendChild(tbody);
    }
    if (!readOnly) {
        document.getElementById("numberOfFiles").value = jsonFiles.length;
        document.getElementById("transferSize").value = returnFileSize(transferSize);
    } else {
        document.getElementById("numberOfFiles").innerHTML = jsonFiles.length;
        document.getElementById("transferSize").innerHTML = returnFileSize(transferSize);
    }
    document.getElementById("fileList").value = JSON.stringify(jsonFiles);
    document.getElementById("file-table").classList.add("table");
    document.getElementById("file-table").classList.add("table-striped");
    document.getElementById("file-table").classList.add("table-bordered");
    $('#files').val('');
}

function changeSent(fileNameToChange) {
    let fileList = JSON.parse(document.getElementById("fileList").value);
    for (var i = 0; i < fileList.length; i++) {
        if (fileList[i].fileName == fileNameToChange) {
            if (fileList[i].fileSent == true) {
                fileList[i].fileSent = false;
            } else {
                fileList[i].fileSent = true;
            }
            break;
        }
    }
    document.getElementById("fileList").value = JSON.stringify(fileList);
}

function changeCommentId(fileIdToChange, val) {
    let fileList = JSON.parse(document.getElementById("fileList").value);
    for (var i = 0; i < fileList.length; i++) {
        if (fileList[i].fileId == fileIdToChange) {
            fileList[i].fileComment = val;
            //alert(val);
            break;
        }
    }
    document.getElementById("fileList").value = JSON.stringify(fileList);
}

function removeFileFromList(fileIdToChange) {
    let fileList = JSON.parse(document.getElementById("fileList").value);
    for (var i = 0; i < fileList.length; i++) {
        if (fileList[i].fileId == fileIdToChange) {
            fileList.splice(i, 1);
            break;
        }
    }
    listFiles(fileList, false);
}

function returnFileSize(bytes) {
    if (bytes < 1024) {
        return bytes + ' bytes';
    } else if (bytes >= 1024 && bytes < 1048576) {
        return (bytes / 1024).toFixed(2) + ' KB';
    } else if (bytes >= 1048576 && bytes < 1073741824) {
        return (bytes / 1048576).toFixed(2) + ' MB';
    } else if (bytes >= 1073741824) {
        return (bytes / 1073741824).toFixed(2) + ' GB';
    }
}

function returnBytes(size) {
    if (size.includes(" bytes")) {
        return Number(size.substring(0, size.indexOf(" bytes")));
    } else if (size.includes(" KB")) {
        const numStr = size.substring(0, size.indexOf(" KB")); 
        return parseFloat(numStr) * 1024;
    } else if (size.includes(" MB")) {
        const numStr = size.substring(0, size.indexOf(" MB"));
        return parseFloat(numStr) * 1024 * 1024;
    } else if (size.includes(" GB")) {
        const numStr = size.substring(0, size.indexOf(" GB"));
        return parseFloat(numStr) * 1024 * 1024 * 1024;
    }
}