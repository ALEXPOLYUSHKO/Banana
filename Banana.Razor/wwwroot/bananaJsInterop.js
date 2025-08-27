// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

export function showPrompt(message) {
  return prompt(message, 'Type anything here');
}

export function getElementRect(elementId)  {
    try {
        const element = document.getElementById(elementId);
        if (element) {
            const rect = element.getBoundingClientRect();
            return {
                x: rect.x,
                y: rect.y,
                width: rect.width,
                height: rect.height
            };
        }
        console.error(`Element with ID '${elementId}' not found.`);
        return null;
    } catch (error) {
        console.error("Error in getBoundingClientRect:", error);
        return null;
    }
}

export function registerViewportChangeCallback(dotnetObject) {
    window.registerViewportChangeCallback = (dotNetObject) => {
        window.addEventListener('load', () => {
            dotNetObject.invokeMethodAsync('OnResize', window.innerWidth, window.innerHeight);
        });
        window.addEventListener('resize', () => {
            dotNetObject.invokeMethodAsync('OnResize', window.innerWidth, window.innerHeight);
        });

        return true;
    }
}
