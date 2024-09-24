window.initializePWAInstall = function () {
    let deferredPrompt;
    const installButton = document.getElementById('installPWA');

    window.addEventListener('beforeinstallprompt', (e) => {
        e.preventDefault();
        deferredPrompt = e;
        installButton.style.display = 'block';

        installButton.addEventListener('click', async () => {
            deferredPrompt.prompt();
            const { outcome } = await deferredPrompt.userChoice;

            if (outcome === 'accepted') {
                console.log('User accepted the install prompt');
            } else {
                console.log('User dismissed the install prompt');
            }

            deferredPrompt = null;
            installButton.style.display = 'none';
        });
    });
};
