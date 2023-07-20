import React, {useState} from "react";
import {Button, Form, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader} from "reactstrap";
import {postWeatherStation, postWxGridPoints} from "../api/remote-ac";

export const OptionsModal = (props: {isOpen: boolean, toggle: () => void}) => {
  console.log(props);
  const {isOpen, toggle} = props;
  const [optionsForm, setOptionsForm] = useState<{[key: string]: string}>({});
  const postOptions = (optionsForm: { [p: string]: string }) => {
    let postWxStationSuccess, postWxGridPointsSuccess = false;
    postWeatherStation({weather_station: optionsForm["weather-station"]})
      .then(response => {
        console.log(response);
        if (response.ok) {
          postWxStationSuccess = true;
        }
      });
    postWxGridPoints({wx_grid_points: optionsForm["grid-points"]})
      .then(response => {
        console.log(response);
        if (response.ok) {
          postWxGridPointsSuccess = true;
        }
      })
    if (postWxStationSuccess && postWxGridPointsSuccess) {
      toggle();
    }
  };
  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(optionsForm);
    postOptions(optionsForm);
  };
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const {target} = e;
    setOptionsForm(prevState => {
      prevState[target.name] = target.value;
      return prevState;
    });
    console.log(optionsForm);
  };
  return (<>
    <Modal isOpen={isOpen} toggle={toggle}>
      <ModalHeader>
        Options
      </ModalHeader>
      <ModalBody>
        <Form id={"options-form"}
              onSubmit={handleSubmit}>
          <FormGroup row>
            <Label for={"weather-station"}>Weather Station</Label>
            <Input id={"weather-station"}
                   name={"weather-station"}
                   onChange={handleChange}
            />
          </FormGroup>
          <FormGroup row>
            <Label for={"grid-points"}>Grid Points</Label>
            <Input id={"grid-points"}
                   name={"grid-points"}
                   onChange={handleChange}
                   placeholder={"110,70"}
            />
          </FormGroup>
        </Form>
      </ModalBody>
      <ModalFooter>
        <Button color={"primary"} form={"options-form"} type={"submit"}>
          Save
        </Button>
        {" "}
        <Button color={"secondary"} onClick={toggle}>
          Cancel
        </Button>
      </ModalFooter>
    </Modal>
  </>);
};