let DATABASE_NAME = "recipi_indexeddb"
let CURRENT_VERSION = 2

export function initialize() {
    console.log("Initalizing indexed db")
    let indexed = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    indexed.onupgradeneeded = function () {
        console.log("Updating indexedDB.")
        let db = indexed.result;
        db.createObjectStore("settings", { keyPath: "id" });
    }
}

export function set(collectionName, value) {
    let db = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    db.onsuccess = function () {
        let transaction = db.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function get(collectionName, id) {
    let request = new Promise((resolve) => {
        let db = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        db.onsuccess = function () {
            let transaction = db.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    let result = await request;

    return result;
}