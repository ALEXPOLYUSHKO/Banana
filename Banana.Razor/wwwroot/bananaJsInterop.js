// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.

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
export function getBrowserDimensions() {
    return {
        width: window.innerWidth,
        height: window.innerHeight
    };
};

let resizeCallback;

export function addResizeListener(dotNetHelper) {
    resizeCallback = () => {
        const width = window.innerWidth;
        const height = window.innerHeight;
        dotNetHelper.invokeMethodAsync('NotifyResize', width, height);
    };
    window.addEventListener('resize', resizeCallback);
}

export function removeResizeListener() {
    if (resizeCallback) {
        window.removeEventListener('resize', resizeCallback);
    }
}
