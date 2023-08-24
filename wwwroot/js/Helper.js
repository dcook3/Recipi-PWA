

window.triggerClick = (el) => el.click();


var preview;
var result;
var recordedChunks = [];
var recording = false;

var recordingFiles = [];
var recordingBlobs = [];
var recordingLengths = [];

var devices;
var deviceIndex = -1;

var canvas;

var postEntry = false;

window.InitializeVideo = async () => {
    
    preview = document.getElementById("preview");
    result = document.getElementById("result");
    canvas = document.createElement("canvas");
    canvas.style.display = "none";
   
    devices = await navigator.mediaDevices.enumerateDevices();
    getNextCamera();
    navigator.mediaDevices.getUserMedia({ video: { deviceId: { exact: devices[deviceIndex].deviceId } }, audio: true })
        .then(stream => {
            // Display webcam in preview video element
            preview.srcObject = stream;

            // Handle browser compatibility issues
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;

            canvas.width = preview.videoWidth;
            canvas.height = preview.videoHeight;

            preview.muted = true;
            // Wait until the preview is visible before recording
            //return new Promise(resolve => preview.onplaying = resolve);
        })

}

var getNextCamera = () => {
    do {
        deviceIndex++;
        if(deviceIndex >= devices.length) {
            deviceIndex = 0;
        }
    } while (devices[deviceIndex].kind != "videoinput")
}

window.NextCamera = () => {
    if (!recording) {
        getNextCamera();
        navigator.mediaDevices.getUserMedia({ video: { deviceId: { exact: devices[deviceIndex].deviceId } }, audio: true })
            .then(stream => {
                // Display webcam in preview video element
                preview.srcObject = stream;

                // Handle browser compatibility issues
                preview.captureStream = preview.captureStream || preview.mozCaptureStream;

                preview.muted = true;
                // Wait until the preview is visible before recording
                //return new Promise(resolve => preview.onplaying = resolve);
            })
    }
}

window.StartRecording = async (stepI) => {
    if (!recording) {
        recording = true;
        return await new Promise((success, error) => {
    
            recordedChunks = [];
            const stream = preview.captureStream();
            recorder = new MediaRecorder(stream);

            var timer;

            recorder.ondataavailable = e => {
                if (e.data.size > 0) {
                    recordedChunks.push(e.data)
                }
            };
            var startTime = new Date().getTime(); 
            recorder.onstop = () => {
                clearTimeout(timer);
                var endTime = new Date().getTime();
                recording = false;
                const blob = new Blob(recordedChunks, { type: 'video/webm' });
                recordingBlobs[stepI] = URL.createObjectURL(blob);
                setSrc(stepI);
                recordingFiles[stepI] = new File([blob], "recording.webm", { type: 'video/webm' });

                
                recordingLengths[stepI] = (Number)(((endTime - startTime) / 1000).toFixed(3));
                
                success("anything")
            }
            recorder.start();
            timer = setTimeout(() => { if (recording) { recorder.stop() } }, 7000);
        })
    }
}


window.UploadRecording = async () => {
    return await new Promise(async (success, error) => {

        result.pause()
        result.currentTime = 0;

        var videoMultiplier = result.videoWidth / result.offsetWidth

        canvas.width = window.innerWidth * videoMultiplier
        canvas.height = window.innerHeight * videoMultiplier

        var mx = ((result.videoWidth - canvas.width) / 2)
        var ctx = canvas.getContext('2d');
        ctx.drawImage(result, mx, 0, canvas.width, result.videoHeight, 0, 0, canvas.width, canvas.height);
    
        canvas.toBlob(async (blob) => {
            var thumbnailFile = new File([blob], "thumbnail.png", { type: 'image/png' });
            
            var thumbnailUrl = await fileUpload(thumbnailFile);

            
            var recordingUrls = [];
            for (var i = 0; i < recordingFiles.length; i++) {
                var url = await fileUpload(recordingFiles[i])
                recordingUrls.push(url);
            }
                 
            success({ thumbnail: thumbnailUrl, recordings: recordingUrls });
            
        }, 'image/png');
    })
}



var setSrc = (i) => {
    result.src = recordingBlobs[i];
    result.load();
}

var postEntryVideoIndex = 0;


var loopResult = () => {
    postEntryVideoIndex++;
    if (postEntryVideoIndex >= recordingBlobs.length) {
        postEntryVideoIndex = 0;
    }
    setSrc(postEntryVideoIndex);
    if (seekBar) {
        if (postEntryVideoIndex != 0) {

            seekBar.children[postEntryVideoIndex].children[0].style.backgroundColor = "#874356";
            seekBar.children[postEntryVideoIndex].children[0].style.borderTopRightRadius = "25px"
            seekBar.children[postEntryVideoIndex].children[0].style.borderBottomRightRadius = "25px"

            seekBar.children[postEntryVideoIndex - 1].children[0].style.backgroundColor = "#C65D7B";
            seekBar.children[postEntryVideoIndex - 1].children[0].style.borderTopRightRadius = "0"
            seekBar.children[postEntryVideoIndex - 1].children[0].style.borderBottomRightRadius = "0"
        }
        else {
            seekBar.children[0].children[0].style.backgroundColor = "#874356";
            seekBar.children[0].children[0].style.width = "0%";
            seekBar.children[0].children[0].style.borderTopRightRadius = "25px"
            seekBar.children[0].children[0].style.borderBottomRightRadius = "25px"

            for (var i = 1; i < seekBar.children.length; i++) {
                seekBar.children[i].children[0].style.backgroundColor = "#eee";
                seekBar.children[i].children[0].style.width = "0%";
                seekBar.children[i].children[0].style.borderTopRightRadius = "0"
                seekBar.children[i].children[0].style.borderBottomRightRadius = "0"
                
            }
        }
    }
}

window.ToPostEntry = () => {
    postEntryVideoIndex = 0;
    result.loop = false;
    setSrc(postEntryVideoIndex);
    result.addEventListener("ended", loopResult)

}
window.BackPostEntry = () => {
    result.removeEventListener("ended", loopResult)
    result.loop = true;
    setSrc(recordingBlobs.length - 1);
}
window.SetStep = (stepI) => {
    if (stepI < recordingBlobs.length) {
        setSrc(stepI);
        return "recorded";
    }
    else {
        return "ready";
    }
}

window.StopRecording = () => {
    recorder.stop();
}

window.TogglePlayback = () => {
    if (result.paused) {
        result.play();
        return true;
    }
    else {
        result.pause();
        return false;
    }
}

var seekVideo;
var clicked
var seekBar
window.InitSeekBar = (videoSelector) => {
    seekVideo = document.querySelector(videoSelector);
    seekBar = document.querySelector("#seek-container");
    var totalLength = recordingLengths.reduce((a, b) => a + b, 0);

    for (var i = 0; i < recordingBlobs.length; i++) {
        var seekPoint = document.createElement("div");
        seekPoint.appendChild(document.createElement("div"));
        seekPoint.classList.add("seek-point");
        seekPoint.style.width = (recordingLengths[i] / totalLength) * 100 + "%";
        seekPoint.setAttribute("data-index", i);
        seekPoint.addEventListener("click", (e) => {
            var target = e.target;
            if (target.parentElement.classList.contains("seek-point")) {
                target = target.parentElement;
            }

            var index = (Number)(target.dataset.index);
            if (index != postEntryVideoIndex) {
                postEntryVideoIndex = index;
                setSrc(index);

                
                for (var n = 0; n < index; n++) {
                    seekBar.children[n].children[0].style.backgroundColor = "#C65D7B";
                    seekBar.children[n].children[0].style.width = "100%";
                    seekBar.children[n].children[0].style.borderTopRightRadius = "0";
                    seekBar.children[n].children[0].style.borderBottomRightRadius = "0";
                }
                n++;
                seekBar.children[index].children[0].style.backgroundColor = "#874356";
                seekBar.children[index].children[0].style.borderTopRightRadius = "25px";
                seekBar.children[index].children[0].style.borderBottomRightRadius = "25px";
                for (n; n < seekBar.children.length; n++) {
                    seekBar.children[n].children[0].style.backgroundColor = "#eee";
                    seekBar.children[n].children[0].style.width = "0%";
                    seekBar.children[n].children[0].style.borderTopRightRadius = "0";
                    seekBar.children[n].children[0].style.borderBottomRightRadius = "0";
                }
            }

            clicked = e
            var left = (e.pageX - e.target.offsetLeft);
            var totalWidth = e.target.offsetWidth;
            var percentage = left / totalWidth;
            var vidTime = recordingLengths[index] * percentage;
            seekVideo.currentTime = vidTime;

            //var play = () => {
            //    seekVideo.play();
            //    seekVideo.removeEventListener("onloadedmetadata", play);
           // }

           // seekVideo.addEventListener("onloadedmetadata", play);
            
        })
        seekBar.appendChild(seekPoint);
    }
    seekBar.children[0].children[0].style.backgroundColor = "#874356";
    seekBar.children[0].children[0].style.borderTopRightRadius = "25px"
    seekBar.children[0].children[0].style.borderBottomRightRadius = "25px"
    
    seekVideo.addEventListener("timeupdate", () => {
        seekBar.children[postEntryVideoIndex].children[0].style.width = (seekVideo.currentTime / seekVideo.duration) * 100 + "%";
    })

    
}