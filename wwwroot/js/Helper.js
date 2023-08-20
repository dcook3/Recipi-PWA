

window.triggerClick = (el) => el.click();

window.ByteArrayToFile = (byteArray) => {
    console.log(byteArray);
    const blob = new Blob(byteArray);
    return URL.createObjectURL(blob);
}
var preview;
var result;
var ready;
var recordedChunks = [];
var recording = false;

var devices;
var deviceIndex = -1;

window.InitializeVideo = async () => {
    
    preview = document.getElementById("preview");
    result = document.getElementById("result");
    ready = document.getElementById("ready");
    //preview.width = window.innerWidth;
    //preview.height = window.innerHeight;
    //result.width = window.innerWidth;
    //result.height = window.innerHeight;
   
    devices = await navigator.mediaDevices.enumerateDevices();
    getNextCamera();
    navigator.mediaDevices.getUserMedia({ video: { deviceId: { exact: devices[deviceIndex].deviceId } }, audio: true })
        .then(stream => {
            // Display webcam in preview video element
            preview.srcObject = stream;

            // Handle browser compatibility issues
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;

            // Wait until the preview is visible before recording
            //return new Promise(resolve => preview.onplaying = resolve);
        })

   

}

var getNextCamera = () => {
    console.log(devices[deviceIndex])
    do {
        deviceIndex++;
        if(deviceIndex >= devices.length) {
            deviceIndex = 0;
        }
    } while (devices[deviceIndex].kind != "videoinput")
    console.log(devices[deviceIndex])
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

                // Wait until the preview is visible before recording
                //return new Promise(resolve => preview.onplaying = resolve);
            })
    }
}

window.StartRecording = async () => {
    if (!recording) {
        recording = true;
        return await new Promise((success, error) => {
    
            recordedChunks = [];
            const stream = preview.captureStream();
            recorder = new MediaRecorder(stream);

            recorder.ondataavailable = e => {
                if (e.data.size > 0) {
                    recordedChunks.push(e.data)
                }
            };

            recorder.onstop = async () => {
                recording = false;
                const blob = new Blob(recordedChunks, { type: 'video/webm' });
                const content = await (new Response(blob).arrayBuffer());
                const contentNums = new Uint8Array(content);
                const contentCodes = new TextDecoder('windows-1252').decode(contentNums);



                result.src = URL.createObjectURL(blob);
                

                
                success(contentCodes)
            }

            recorder.start();
            setTimeout(() => { if (recording) { recorder.stop() } }, 7000);
        })
    }
}

window.StopRecording = () => {
    recorder.stop();
}

window.TogglePlayback = () => {
    if (result.paused) {
        result.play();
    }
    else {
        result.pause();
    }
}