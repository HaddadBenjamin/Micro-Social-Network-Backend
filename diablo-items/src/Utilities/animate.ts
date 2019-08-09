import jquery from "jquery";

export default function scrollTo(cssSelector : string)
{
    jquery('html, body').animate(
        {
            // @ts-ignore
            scrollTop: jquery(cssSelector).offset().top
        }, 800, 'swing');
}