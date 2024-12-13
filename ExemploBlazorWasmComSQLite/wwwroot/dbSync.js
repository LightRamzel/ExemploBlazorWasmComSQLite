(async function () {

    const db = {
        init: false,
        cache: await caches.open('ExemploBlazorWasmComSQLite')
    };

    window.db = db;

    db.synchronizeDbWithCache = async function (filename) {

        const backupPath = `/${filename}`;
        const cachePath = `/data/cache${backupPath.split('.')[0]}.db`;
        console.log(`Processing ${backupPath}...`);

        if (!db.init) {

            db.init = true;

            console.log("Checking cache...");

            const resp = await db.cache.match(cachePath);

            if (resp && resp.ok) {

                const res = await resp.arrayBuffer();

                if (res) {
                    window.Module.FS.writeFile(backupPath, new Uint8Array(res));
                    const size = window.Module.FS.stat(backupPath).size;
                    console.log(`Restored ${size} bytes from cache.`);
                    return 0;
                }
            }
            console.log("No cache available.");
            return -1;
        }

        if (window.Module.FS.analyzePath(backupPath).exists) {

            // give files time to flush
            const waitFlush = new Promise((done, _) => {
                setTimeout(done, 10);
            });

            await waitFlush;

            const data = window.Module.FS.readFile(backupPath);

            const blob = new Blob([data], {
                type: 'application/octet-stream',
                ok: true,
                status: 200
            });

            const headers = new Headers({
                'content-length': blob.size
            });

            const response = new Response(blob, {
                headers
            });

            await db.cache.put(cachePath, response);

            console.log("Data cached.");
            window.Module.FS.unlink(backupPath);
            const exists = window.Module.FS.analyzePath(backupPath).exists;
            console.log(`${backupPath} exists: ${exists}`);
            return 1;
        }
        else {
            console.log("File not found.");
        }
        return -1;
    };

    db.downloadDbInCache = async function (filename) {

        const backupPath = `/${filename}`;
        const cachePath = `/data/cache${backupPath.split('.')[0]}.db`;
        console.log(`Processing ${backupPath}...`);

        if (db.init) {

            console.log("Checking cache backup...");

            const resp = await db.cache.match(cachePath);

            if (resp && resp.ok) {

                const res = await resp.blob();

                // Create a URL for the blob
                const url = window.URL.createObjectURL(res);

                // Create a temporary anchor element to trigger the download
                const a = document.createElement('a');
                a.href = url;
                a.download = "database_backup.sqlite3";
                document.body.appendChild(a);
                a.click();

                // Clean up
                document.body.removeChild(a);
                window.URL.revokeObjectURL(url);

                console.log("Download completed.");
                return 0;
            }
            console.log("No cache available.");
            return -1;
        }

        return -1;
    };
})();