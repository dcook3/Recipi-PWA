var preview;
var recordedChunks = [];
var recording = false;

var videos = {};

// recordingLengths
// recordingFiles
// recordingBlobs

var devices;
var deviceIndex = -1;

var canvas;

var postEntry = false;

window.InitializeVideo = async () => {

    preview = document.getElementById("preview");
    videos['recording'] = {
        video: document.getElementById("result"),
        videoIndex: 0,
        urls: [],
        lengths: []
    };
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
        if (deviceIndex >= devices.length) {
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
            .catch(err => {
                window.NextCamera();
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

                videos['recording'].urls[stepI] = URL.createObjectURL(blob);
                videos['recording'].lengths[stepI] = (Number)(((endTime - startTime) / 1000).toFixed(3));
                
                setSrc('recording', stepI);

                

                success("anything")
            }
            recorder.start();
            timer = setTimeout(() => { if (recording) { recorder.stop() } }, 7000);
        })
    }
}


window.UploadRecording = async () => {
    return await new Promise(async (success, error) => {

        videos['recording'].video.pause()
        videos['recording'].video.currentTime = 0;

        var videoMultiplier = videos['recording'].video.videoWidth / videos['recording'].video.offsetWidth

        canvas.width = window.innerWidth * videoMultiplier
        canvas.height = window.innerHeight * videoMultiplier

        var mx = ((videos['recording'].video.videoWidth - canvas.width) / 2)
        var ctx = canvas.getContext('2d');
        ctx.drawImage(videos['recording'].video, mx, 0, canvas.width, videos['recording'].video.videoHeight, 0, 0, canvas.width, canvas.height);

        canvas.toBlob(async (blob) => {
            var thumbnailFile = new File([blob], "thumbnail.png", { type: 'image/png' });

            var thumbnailUrl = await fileUpload(thumbnailFile);

            var recordingUrls = [];
            for (var i = 0; i < videos['recording'].urls.length; i++) {
                var blob = await fetch(videos['recording'].urls[i]).then(r => r.blob());
                var url = await fileUpload(new File([blob], "recording.webm", { type: 'video/webm' }))
                recordingUrls.push(url);
            }

            success({ thumbnail: thumbnailUrl, recordings: recordingUrls });

        }, 'image/png');
    })
}



var setSrc = (vid, i) => {
    videos[vid].video.src = videos[vid].urls[i];
    videos[vid].video.load();
}


var loopResult = (vid) => {
    videos[vid].videoIndex++;
    if (videos[vid].videoIndex >= videos[vid].urls.length) {
        videos[vid].videoIndex = 0;
    }
    setSrc(vid, videos[vid].videoIndex);
    if (videos[vid].seekBar) {
        if (videos[vid].videoIndex != 0) {

            videos[vid].seekBar.children[videos[vid].videoIndex].children[0].style.backgroundColor = "#874356";
            videos[vid].seekBar.children[videos[vid].videoIndex].children[0].style.borderTopRightRadius = "25px"
            videos[vid].seekBar.children[videos[vid].videoIndex].children[0].style.borderBottomRightRadius = "25px"

            videos[vid].seekBar.children[videos[vid].videoIndex - 1].children[0].style.backgroundColor = "#C65D7B";
            videos[vid].seekBar.children[videos[vid].videoIndex - 1].children[0].style.borderTopRightRadius = "0"
            videos[vid].seekBar.children[videos[vid].videoIndex - 1].children[0].style.borderBottomRightRadius = "0"
        }
        else {
            videos[vid].seekBar.children[0].children[0].style.backgroundColor = "#874356";
            videos[vid].seekBar.children[0].children[0].style.width = "0%";
            videos[vid].seekBar.children[0].children[0].style.borderTopRightRadius = "25px"
            videos[vid].seekBar.children[0].children[0].style.borderBottomRightRadius = "25px"

            for (var i = 1; i < videos[vid].seekBar.children.length; i++) {
                videos[vid].seekBar.children[i].children[0].style.backgroundColor = "#eee";
                videos[vid].seekBar.children[i].children[0].style.width = "0%";
                videos[vid].seekBar.children[i].children[0].style.borderTopRightRadius = "0"
                videos[vid].seekBar.children[i].children[0].style.borderBottomRightRadius = "0"

            }
        }
    }
}

window.ToPostEntry = () => {
    videos['recording'].videoIndex = 0;
    videos['recording'].video.loop = false;
    setSrc('recording', 0);
    videos['recording'].video.addEventListener("ended", () => loopResult("recording"))

}
window.BackPostEntry = () => {
    videos['recording'].video.removeEventListener("ended", () => loopResult("recording"))
    videos['recording'].video.loop = true;
    setSrc('recording', videos['recording'].urls.length - 1);
}
window.SetStep = (stepI) => {
    if (stepI < videos['recording'].urls.length) {
        setSrc('recording', stepI);
        return "recorded";
    }
    else {
        return "ready";
    }
}

window.StopRecording = () => {
    recorder.stop();
}

window.TogglePlayback = (vid) => {
    if (videos[vid].video.paused) {
        videos[vid].video.play();
        return true;
    }
    else {
        videos[vid].video.pause();
        return false;
    }
}

var vidEls;
window.InitPost = async (vid, recordings, objectReference, focused) => {

    videos[vid] = {
        video: document.getElementById(vid + "-post-video"),
        videoIndex: 0,
        urls: recordings,
        lengths: [],
        objectReference: objectReference
    };
    
    videos[vid].video.src = recordings[0];
    videos[vid].video.load();

    if (focused) {
        videos[vid].video.paused = false;
        videos[vid].video.play();
    }
    else{
        videos[vid].video.paused = true;
        videos[vid].video.pause();
    }
    videos[vid].videoIndex = 0;

    var videoEls = [] 

    console.log(recordings);

    await new Promise(resolve => {

        var loadVideo = async (e) => {
            var i = e.target.getAttribute("i");
            if (i <= recordings.length) {
                if (i < recordings.length-1) {
                    i++;

                    videoEls[i] = document.createElement('video');
                    videoEls[i].addEventListener("loadedmetadata", loadVideo);

                    //videoEls[i].preload = "metadata";
                    videoEls[i].setAttribute("i", i);
                    videoEls[i].src = recordings[i];
                    videoEls[i].load();
                }
                else {
                    resolve();
                }
            }
        }
        videoEls[0] = document.createElement('video');
        videoEls[0].addEventListener("loadedmetadata", loadVideo);

        //videoEls[0].preload = "metadata";
        videoEls[0].setAttribute("i", 0);
        videoEls[0].src = recordings[0];
        videoEls[0].load();
    })
    //await sleep(500);
    
    vidEls = videoEls
    for (var i = 0; i < videoEls.length; i++) {
        var checks = 0;
        while (checks < 100 && (videoEls[i].duration === Infinity || videoEls[i].duration === NaN)) {
            await new Promise(r => setTimeout(r, 100));
            console.log("checking")
            videoEls[i].currentTime = 10000000 * Math.random();
            checks++;
        }
        videos[vid].lengths[i] = videoEls[i].duration;
    }
    window.InitSeekBar(vid);
    videos[vid].video.addEventListener("ended", () => {
        loopResult(vid)
        videos[vid].objectReference.invokeMethodAsync("SetStep", videos[vid].videoIndex)
    })
    
}


window.InitSeekBar = (vid) => {
    videos[vid].seekBar = document.getElementById(vid + "-seek-container");
    if (videos[vid].seekBar) {
        videos[vid].seekBar.innerHTML = "";
    }
    var totalLength = videos[vid].lengths.reduce((a, b) => a + b, 0);

    for (var i = 0; i < videos[vid].urls.length; i++) {
        var seekPoint = document.createElement("div");
        seekPoint.appendChild(document.createElement("div"));
        seekPoint.classList.add("seek-point");
        console.log("Seek Point Width: " + (videos[vid].lengths[i] / totalLength) * 100 + "%")
        seekPoint.style.width = (videos[vid].lengths[i] / totalLength) * 100 + "%";
        seekPoint.setAttribute("data-index", i);
        seekPoint.addEventListener("click", (e) => {
            var target = e.target;
            if (target.parentElement.classList.contains("seek-point")) {
                target = target.parentElement;
            }
            var paused = videos[vid].video.paused;
            var index = (Number)(target.dataset.index);
            if (index != videos[vid].videoIndex) {
                videos[vid].videoIndex = index;
                setSrc(vid, index);


                for (var n = 0; n < index; n++) {
                    videos[vid].seekBar.children[n].children[0].style.backgroundColor = "#C65D7B";
                    videos[vid].seekBar.children[n].children[0].style.width = "100%";
                    videos[vid].seekBar.children[n].children[0].style.borderTopRightRadius = "0";
                    videos[vid].seekBar.children[n].children[0].style.borderBottomRightRadius = "0";
                }
                n++;
                videos[vid].seekBar.children[index].children[0].style.backgroundColor = "#874356";
                videos[vid].seekBar.children[index].children[0].style.width = "0%";
                videos[vid].seekBar.children[index].children[0].style.borderTopRightRadius = "25px";
                videos[vid].seekBar.children[index].children[0].style.borderBottomRightRadius = "25px";
                for (n; n < videos[vid].seekBar.children.length; n++) {
                    videos[vid].seekBar.children[n].children[0].style.backgroundColor = "#eee";
                    videos[vid].seekBar.children[n].children[0].style.width = "0%";
                    videos[vid].seekBar.children[n].children[0].style.borderTopRightRadius = "0";
                    videos[vid].seekBar.children[n].children[0].style.borderBottomRightRadius = "0";
                }
            }

            var left = (e.pageX - e.target.offsetLeft);
            var totalWidth = e.target.offsetWidth;
            var percentage = left / totalWidth;
            var vidTime = videos[vid].lengths[index] * percentage;
            videos[vid].video.currentTime = 0;

            if (paused) {
                videos[vid].video.paused = true;
                videos[vid].video.pause();
            }
            else {
                videos[vid].video.paused = false;
                videos[vid].video.play();
            }

            //var play = () => {
            //    seekVideo.play();
            //    seekVideo.removeEventListener("onloadedmetadata", play);
            // }

            // seekVideo.addEventListener("onloadedmetadata", play);

        })
        if (videos[vid].seekBar) {
            videos[vid].seekBar.appendChild(seekPoint);
        }
    }
    if (videos[vid].seekBar) {
        videos[vid].seekBar.children[0].children[0].style.backgroundColor = "#874356";
        videos[vid].seekBar.children[0].children[0].style.borderTopRightRadius = "25px"
        videos[vid].seekBar.children[0].children[0].style.borderBottomRightRadius = "25px"
    }

    videos[vid].video.addEventListener("timeupdate", () => {
        if (videos[vid]?.seekBar?.children[videos[vid]?.videoIndex]?.children[0]) {
            videos[vid].seekBar.children[videos[vid].videoIndex].children[0].style.width = (videos[vid].video.currentTime / videos[vid].lengths[videos[vid].videoIndex]) * 100 + "%";
        }
    })


}