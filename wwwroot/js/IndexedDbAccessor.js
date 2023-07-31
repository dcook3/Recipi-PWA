export function initialize() {
    let indexed = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
    indexed.onupgradeneeded = function () {
        let db = indexed.result;
        db.createObjectStore("settings", { keyPath: "id" });
    }
}

export function set(collectionName, value) {
    let blazorSchoolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    blazorSchoolIndexedDb.onsuccess = function () {
        let transaction = blazorSchoolIndexedDb.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
    }
}

export async function get(collectionName, id) {
    let request = new Promise((resolve) => {
        let blazorSchoolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        blazorSchoolIndexedDb.onsuccess = function () {
            let transaction = blazorSchoolIndexedDb.result.transaction(collectionName, "readonly");
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