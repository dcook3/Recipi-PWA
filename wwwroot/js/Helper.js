

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
const recording = false;

window.InitializeVideo = () => {
    
    preview = document.getElementById("preview");
    result = document.getElementById("result");
    ready = document.getElementById("ready");
    preview.width = window.innerWidth;
    preview.height = window.innerHeight;
    result.width = window.innerWidth;
    result.height = window.innerHeight;
    navigator.mediaDevices.getUserMedia({ video: true, audio: true })
        .then(stream => {
            // Display webcam in preview video element
            preview.srcObject = stream;

            // Handle browser compatibility issues
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;

            // Wait until the preview is visible before recording
            return new Promise(resolve => preview.onplaying = resolve);
        })

   

}

window.StartRecording = () => {
    navigator.mediaDevices.getUserMedia({ video: true, audio: true })
        .then(stream => {
            // Display webcam in preview video element
            preview.srcObject = stream;

            // Handle browser compatibility issues
            preview.captureStream = preview.captureStream || preview.mozCaptureStream;

            // Wait until the preview is visible before recording
            return new Promise(resolve => preview.onplaying = resolve);
        })
        .then(() => {
            recordedChunks = [];
            const stream = preview.captureStream();
            recorder = new MediaRecorder(stream);
            recorder.ondataavailable = event => {
                    recordedChunks.push(event.data)
            };
            recorder.start();
        })
}

window.StopRecording = () => {
    

    recorder.stop();

    recorder.onstop = () => {
        const blob = new Blob(recordedChunks, { type: 'video/webm' });

        result.src = URL.createObjectURL(blob);
        ready.click();
    }

    
    

}