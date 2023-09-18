"use strict";(self.webpackChunkclient=self.webpackChunkclient||[]).push([[343],{2343:(M,p,i)=>{i.r(p),i.d(p,{AccountModule:()=>w});var c=i(6814),b=i(6208),u=i(8733),r=i(95),t=i(4769),d=i(6448),g=i(9847);let y=(()=>{class e{constructor(o,n,s){this.accountService=o,this.router=n,this.activatedRoute=s,this.loginForm=new r.cw({email:new r.NI("",[r.kI.required,r.kI.email]),password:new r.NI("",r.kI.required)}),this.returnUrl=this.activatedRoute.snapshot.queryParams.returnUrl||"/home"}onSubmit(){this.accountService.login(this.loginForm.value).subscribe({next:()=>this.router.navigateByUrl(this.returnUrl)})}}return e.\u0275fac=function(o){return new(o||e)(t.Y36(d.B),t.Y36(u.F0),t.Y36(u.gz))},e.\u0275cmp=t.Xpm({type:e,selectors:[["app-login"]],decls:11,vars:7,consts:[[1,"d-flex","justify-content-center","mt-5"],[1,"col-3"],[3,"formGroup","ngSubmit"],[1,"text-center","mb-4"],[1,"mb-3"],[3,"formControl","label"],[3,"formControl","type","label"],[1,"d-grid"],["type","submit",1,"btn","btn-lg","btn-primary","mt-3",3,"disabled"]],template:function(o,n){1&o&&(t.TgZ(0,"div",0)(1,"div",1)(2,"form",2),t.NdJ("ngSubmit",function(){return n.onSubmit()}),t.TgZ(3,"div",3)(4,"h1",4),t._uU(5,"Login"),t.qZA()(),t._UZ(6,"app-text-input",5)(7,"app-text-input",6),t.TgZ(8,"div",7)(9,"button",8),t._uU(10,"Sign In"),t.qZA()()()()()),2&o&&(t.xp6(2),t.Q6J("formGroup",n.loginForm),t.xp6(4),t.Q6J("formControl",n.loginForm.controls.email)("label","Email Address"),t.xp6(1),t.Q6J("formControl",n.loginForm.controls.password)("type","password")("label","Password"),t.xp6(2),t.Q6J("disabled",n.loginForm.invalid))},dependencies:[r._Y,r.JJ,r.JL,r.oH,r.sg,g.t]}),e})();var C=i(6321),x=i(9360),F=i(8251),S=i(8180),T=i(4664),A=i(7398),Z=i(4716);function I(e,l){if(1&e&&(t.TgZ(0,"li"),t._uU(1),t.qZA()),2&e){const o=l.$implicit;t.xp6(1),t.hij(" ",o," ")}}function U(e,l){if(1&e&&(t.TgZ(0,"ul",10),t.YNc(1,I,2,1,"li",11),t.qZA()),2&e){const o=t.oxw();t.xp6(1),t.Q6J("ngForOf",o.errors)}}const R=[{path:"login",component:y},{path:"register",component:(()=>{class e{constructor(o,n,s){this.fb=o,this.accountService=n,this.router=s,this.complexPassword="(?=^.{6,10}$)(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*s).*$",this.errors=null,this.registerForm=this.fb.group({displayName:["",r.kI.required],email:["",[r.kI.required,r.kI.email],[this.validateEmailNotTaken()]],password:["",[r.kI.required,r.kI.pattern(this.complexPassword)]]})}onSubmit(){this.accountService.register(this.registerForm.value).subscribe({next:()=>this.router.navigateByUrl("/home"),error:o=>this.errors=o.errors})}validateEmailNotTaken(){return o=>o.valueChanges.pipe(function J(e,l=C.z){return(0,x.e)((o,n)=>{let s=null,m=null,f=null;const h=()=>{if(s){s.unsubscribe(),s=null;const a=m;m=null,n.next(a)}};function Y(){const a=f+e,v=l.now();if(v<a)return s=this.schedule(void 0,a-v),void n.add(s);h()}o.subscribe((0,F.x)(n,a=>{m=a,f=l.now(),s||(s=l.schedule(Y,e),n.add(s))},()=>{h(),n.complete()},void 0,()=>{m=s=null}))})}(1e3),(0,S.q)(1),(0,T.w)(()=>this.accountService.checkEmailExists(o.value).pipe((0,A.U)(n=>n?{emailExists:!0}:null),(0,Z.x)(()=>o.markAsTouched))))}}return e.\u0275fac=function(o){return new(o||e)(t.Y36(r.qu),t.Y36(d.B),t.Y36(u.F0))},e.\u0275cmp=t.Xpm({type:e,selectors:[["app-register"]],decls:13,vars:10,consts:[[1,"d-flex","justify-content-center","mt-5"],[1,"col-3"],[3,"formGroup","ngSubmit"],[1,"text-center","mb-4"],[1,"mb-3"],[3,"formControl","label"],[3,"formControl","type","label"],["class","text-danger list-unstyled",4,"ngIf"],[1,"d-grid"],["type","submit",1,"btn","btn-lg","btn-primary","mt-3",3,"disabled"],[1,"text-danger","list-unstyled"],[4,"ngFor","ngForOf"]],template:function(o,n){1&o&&(t.TgZ(0,"div",0)(1,"div",1)(2,"form",2),t.NdJ("ngSubmit",function(){return n.onSubmit()}),t.TgZ(3,"div",3)(4,"h1",4),t._uU(5,"Sign up"),t.qZA()(),t._UZ(6,"app-text-input",5)(7,"app-text-input",5)(8,"app-text-input",6),t.YNc(9,U,2,1,"ul",7),t.TgZ(10,"div",8)(11,"button",9),t._uU(12,"Sign Up"),t.qZA()()()()()),2&o&&(t.xp6(2),t.Q6J("formGroup",n.registerForm),t.xp6(4),t.Q6J("formControl",n.registerForm.controls.displayName)("label","Display Name"),t.xp6(1),t.Q6J("formControl",n.registerForm.controls.email)("label","Email Address"),t.xp6(1),t.Q6J("formControl",n.registerForm.controls.password)("type","password")("label","Password"),t.xp6(1),t.Q6J("ngIf",n.errors),t.xp6(2),t.Q6J("disabled",n.registerForm.invalid))},dependencies:[c.sg,c.O5,r._Y,r.JJ,r.JL,r.oH,r.sg,g.t]}),e})()}];let N=(()=>{class e{}return e.\u0275fac=function(o){return new(o||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[u.Bz.forChild(R),u.Bz]}),e})();var Q=i(4733);let w=(()=>{class e{}return e.\u0275fac=function(o){return new(o||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[c.ez,N,b.m,Q.HomeModule]}),e})()}}]);