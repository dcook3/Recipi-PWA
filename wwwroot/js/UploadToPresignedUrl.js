//Takes a file and uploads it to an S3 Bucket with a presigned url. Respond with a link pointing to the file stored in the bucket.
async function fileUpload(file) {
    const url = await fetch("https://www.recipiapp.com/api/MediaUpload", { headers: { "Authorization": 'Bearer ' + localStorage.getItem('token') } }).then(response => response.text());
    // post the image direclty to the s3 bucket
    await fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": file.type,
        },
        body: file
    });

    const imageUrl = url.split('?')[0];
    return imageUrl;
}
