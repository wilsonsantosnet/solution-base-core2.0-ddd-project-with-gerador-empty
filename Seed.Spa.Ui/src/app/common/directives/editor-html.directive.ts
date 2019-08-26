import { Directive, ElementRef, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { NgModel } from '@angular/forms';
import { GlobalService, NotificationParameters } from "../../global.service";

declare var $,tinymce: any;

@Directive({
    selector: '[editorhtml]',
    providers: [NgModel]
})

export class EditorHtmlDiretive implements OnInit {

    @Output() editorKeyup = new EventEmitter<number>();
    editor: any;
    _notificationEmitter: EventEmitter<NotificationParameters>;

    constructor(private el: ElementRef, private ngModel: NgModel) {

        this._notificationEmitter = new EventEmitter<NotificationParameters>();    

    }

    ngOnInit() {
        this.render();

        this._notificationEmitter = GlobalService.getNotificationEmitter().subscribe((not: any) => {
            if (not.event == "edit" || not.event == "create") {
                let element = $(this.el.nativeElement);
                var selector = element.attr("editorhtml");
                var name = element.attr("name");
                if (tinymce.get(selector)) {
                    if (not.event == "edit")
                        tinymce.get(selector).getBody().innerHTML = not.data.model[name];
                    if (not.event == "create")
                      tinymce.get(selector).getBody().innerHTML = "";
                }
            }
        })
    }

    render() {

        let element = $(this.el.nativeElement);

        tinymce.init({
            selector: '[editorhtml=' + element.attr("editorhtml") + ']',
            plugins: ['link', 'paste', 'lists', 'advlist'],
            skin_url: '/assets/css/skins/lightgray',
            setup: (editor : any) => {
                editor.on('change', () => {
                    const content = editor.getContent();
                    this.editor = editor;
                    this.editorKeyup.emit(content);
                  });
            }
        });
    }

    ngOnDestroy() {

        if (tinymce)
            tinymce.remove(this.editor);

        if (this._notificationEmitter)
            this._notificationEmitter.unsubscribe();
    }
}
