import { Component, OnInit } from '@angular/core';

declare var $: any;

@Component({
  selector: 'app-banner',
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss']
})

export class BannerComponent implements OnInit {

  constructor() { }

  ngOnInit() {

    $('.owl-carousel').owlCarousel({
      loop: true,
      margin: 10,
      nav: false,
      items: 1,
      autoplay: true,
      autoplayTimeout: 5000,
      autoplayHoverPause: true
    })
  }

}
