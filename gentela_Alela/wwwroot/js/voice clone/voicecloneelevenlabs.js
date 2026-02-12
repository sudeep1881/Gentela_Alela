let mediaRecorder;
let audioChunks = [];
let audioStream = null;

async function startRecording()
{

    audioChunks = [];
    
    document.getElementById("recordStatus").innerText = "Recording...";
    audioStream = await navigator.mediaDevices.getUserMedia({ audio: true });
    mediaRecorder = new MediaRecorder(audioStream, { mimeType: "audio/webm" });
    mediaRecorder.ondataavailable = e => audioChunks.push(e.data);
    mediaRecorder.onstop = () => {
        document.getElementById("recordStatus").innerText = "Recording stopped";
        audioStream.getTracks().forEach(t => t.stop());
    };
    mediaRecorder.start();
    document.getElementById("recordBtn").disabled = true;
    document.getElementById("stopBtn").disabled = false;
}

function ResetRecord() {
    if (mediaRecorder && mediaRecorder.state === "recording") {
        mediaRecorder.stop();
    }
    audioChunks = []; document.getElementById("voiceName").value = "";
    document.getElementById("recordStatus").innerText = " ";
    document.getElementById("recordBtn").disabled = false;
    document.getElementById("stopBtn").disabled = true;
}
function stopRecording() {
    if (mediaRecorder && mediaRecorder.state === "recording") mediaRecorder.stop();
    document.getElementById("recordBtn").disabled = false;
    document.getElementById("stopBtn").disabled = true;
}

async function submitRecordedVoice() {
    if (audioChunks.length === 0) {
        alert("No audio recorded"); return;
    }
    const voiceName = document.getElementById("voiceName").value;
    if (!voiceName) {
        alert("Please enter voice name");
        return;
    }
    document.getElementById("recordStatus").innerText = "Uploading...";
    // IMPORTANT FIX: force clean MIME
    const blob = new Blob(audioChunks,
        {
            type: "audio/webm"
        });
    const formData = new FormData();
    // MUST MATCH controller parameter name
    formData.append("audioFile", blob, "voice.webm");
    // MUST MATCH controller parameter name
    formData.append("voiceName", voiceName);
    const res = await fetch("/Admin/UploadVoice",
        {
            method: "POST", body: formData
        });
    const text = await res.text();
    if (!res.ok) {
        alert("Upload failed:\n\n" + text);
        document.getElementById("recordStatus").innerText = ""; return;
    }
    alert("🎉 Voice uploaded successfully to ElevenLabs");
    document.getElementById("recordStatus").innerText = "Upload complete";
}
function showPopup() {
    let text = document.getElementById("recordText").value; document.getElementById("popupContent").innerText = text;
    document.getElementById("textPopup").style.display = "block";
}
function closePopup() {
    document.getElementById("textPopup").style.display = "none";
}

//delete Voice from the elevenLabs

async function deleteVoice(voiceId) {

    if (!confirm("Are you sure you want to delete this voice?"))
        return;

    const res = await fetch('/Admin/DeleteVoice', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: 'voiceId=' + encodeURIComponent(voiceId)
    });

    if (!res.ok) {
        alert("Delete failed");
        return;
    }

    alert("Voice deleted successfully ✅");

    location.reload(); // refresh table
}

