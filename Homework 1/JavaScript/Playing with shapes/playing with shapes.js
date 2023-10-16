class FigureDesigner {

    canvas = null;
    ctx = canvas.getContext("2d"); 

    constructor(canvas) {
        this.canvas = canvas;
    }

    drawPoint(x, y, thickness, color) { 
        this.ctx.beginPath();
        this.ctx.arc(x, y, thickness, 0, Math.PI * 2);
        this.ctx.fillStyle = color; 
        this.ctx.fill(); 
    }

    drawLine(startx, starty, endx, endy, color) { 
        this.ctx.beginPath();
        this.ctx.moveTo(startx, starty); 
        this.ctx.lineTo(endx, endy);
        this.ctx.strokeStyle = color; 
        this.ctx.stroke(); 
    }

    drawCircle(x, y, thickness, fillcolor, strokecolor) { 
        this.drawPoint(x, y, thickness, fillcolor);
        this.ctx.strokeStyle = strokecolor; 
        this.ctx.stroke(); 
    }

    drawRectangle(x, y, width, height, fillcolor, strokecolor) { 
        this.ctx.beginPath();
        this.ctx.rect(x, y, width, height);
        this.ctx.strokeStyle = fillcolor;
        this.ctx.stroke();
        this.ctx.fillStyle = strokecolor;
        this.ctx.fill();
    }

}